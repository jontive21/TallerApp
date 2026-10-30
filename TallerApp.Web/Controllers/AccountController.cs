using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerApp.Web.Models;
using TallerApp.Web.Data;

namespace TallerApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepositorioUsuario _repoUsuario;
        private readonly IWebHostEnvironment _entorno;

        private static readonly string[] ExtensionesPermitidas = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
        private const long TamanioMaximoBytes = 2 * 1024 * 1024; // 2 MB

        public AccountController(IRepositorioUsuario repoUsuario, IWebHostEnvironment entorno)
        {
            _repoUsuario = repoUsuario;
            _entorno = entorno;
        }

        // MÉTODO TEMPORAL PARA CREAR USUARIO ADMIN DE PRUEBA
        [HttpGet]
        public IActionResult CrearAdminTest()
        {
            var usuario = new Usuario
            {
                NombreCompleto = "Administrador",
                Email = "admin@tallerapp.com",
                PasswordHash = PasswordHelper.GenerarHash("Admin123"),
                Rol = "Admin"
            };
            _repoUsuario.Alta(usuario);
            return Content("✅ Usuario Admin creado! Email: admin@tallerapp.com | Password: Admin123");
        }

        // ===================== LOGIN / LOGOUT =====================
[HttpGet]
public IActionResult DiagnosticarLogin()
{
    string emailPrueba = "admin@tallerapp.com";
    string passwordPrueba = "Admin123";

    var usuario = _repoUsuario.ObtenerPorEmail(emailPrueba);
    
    if (usuario == null)
    {
        return Content($"❌ ERROR: No se encontró ningún usuario con el email '{emailPrueba}' en la base de datos.");
    }

    bool esValido = PasswordHelper.VerificarHash(passwordPrueba, usuario.PasswordHash);
    
    if (esValido)
    {
        return Content($"✅ ÉXITO: El usuario '{usuario.Email}' fue encontrado y la contraseña '{passwordPrueba}' es CORRECTA.\nHash en BD: {usuario.PasswordHash}");
    }
    else
    {
        return Content($"❌ ERROR DE CONTRASEÑA: El usuario '{usuario.Email}' fue encontrado, pero la contraseña '{passwordPrueba}' NO coincide con el hash almacenado.\nHash en BD: {usuario.PasswordHash}");
    }
}
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var usuario = _repoUsuario.ObtenerPorEmail(model.Email);

                if (usuario != null && PasswordHelper.VerificarHash(model.Password, usuario.PasswordHash))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                        new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Role, usuario.Rol)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return LocalRedirect(returnUrl ?? "/");
                }

                ModelState.AddModelError(string.Empty, "Usuario o contraseÃ±a incorrectos.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // ===================== PERFIL PROPIO =====================

        [Authorize]
        public IActionResult Perfil()
        {
            var usuario = ObtenerUsuarioActual();
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // ----- Editar datos personales -----
        [Authorize]
        [HttpGet]
        public IActionResult EditarDatos()
        {
            var usuario = ObtenerUsuarioActual();
            if (usuario == null) return NotFound();

            var model = new EditarDatosViewModel
            {
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarDatos(EditarDatosViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = ObtenerUsuarioActual();
            if (usuario == null) return NotFound();

            try
            {
                usuario.NombreCompleto = model.NombreCompleto;
                usuario.Email = model.Email;
                _repoUsuario.Modificacion(usuario);

                // El nombre vive en la cookie de sesiÃ³n: se vuelve a emitir para que el navbar lo refleje.
                await RefrescarSesionAsync(usuario);

                TempData["Mensaje"] = "Datos personales actualizados correctamente.";
                return RedirectToAction(nameof(Perfil));
            }
            catch
            {
                ViewBag.Error = "No se pudieron actualizar los datos (posible email duplicado).";
                return View(model);
            }
        }

        // ----- Cambiar contraseÃ±a -----
        [Authorize]
        [HttpGet]
        public IActionResult CambiarClave()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambiarClave(CambiarClaveViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = ObtenerUsuarioActual();
            if (usuario == null) return NotFound();

            if (!PasswordHelper.VerificarHash(model.ClaveActual, usuario.PasswordHash))
            {
                ModelState.AddModelError("ClaveActual", "La contraseÃ±a actual es incorrecta.");
                return View(model);
            }

            try
            {
                usuario.PasswordHash = PasswordHelper.GenerarHash(model.ClaveNueva);
                _repoUsuario.Modificacion(usuario);

                TempData["Mensaje"] = "ContraseÃ±a cambiada correctamente.";
                return RedirectToAction(nameof(Perfil));
            }
            catch
            {
                ViewBag.Error = "No se pudo cambiar la contraseÃ±a.";
                return View(model);
            }
        }

        // ----- Cambiar Avatar -----
        [Authorize]
        [HttpGet]
        public IActionResult CambiarAvatar()
        {
            var usuario = ObtenerUsuarioActual();
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambiarAvatar(IFormFile? AvatarFile)
        {
            var usuario = ObtenerUsuarioActual();
            if (usuario == null) return NotFound();

            if (AvatarFile == null || AvatarFile.Length == 0)
            {
                ViewBag.Error = "DebÃ©s seleccionar una imagen.";
                return View(usuario);
            }

            try
            {
                var ruta = GuardarAvatar(AvatarFile);
                if (ruta == null)
                {
                    ViewBag.Error = "Formato o tamaÃ±o de imagen no vÃ¡lido (mÃ¡x 2MB, jpg/png/webp/gif).";
                    return View(usuario);
                }

                // ReciÃ©n ahora que el nuevo se guardÃ³ bien, se borra el anterior.
                EliminarArchivoSiExiste(usuario.Avatar);
                usuario.Avatar = ruta;
                _repoUsuario.Modificacion(usuario);

                TempData["Mensaje"] = "Avatar actualizado correctamente.";
                return RedirectToAction(nameof(Perfil));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al subir el avatar: " + ex.Message;
                return View(usuario);
            }
        }

        // ----- Eliminar Avatar -----
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarAvatar()
        {
            var usuario = ObtenerUsuarioActual();
            if (usuario == null) return NotFound();

            try
            {
                EliminarArchivoSiExiste(usuario.Avatar);
                usuario.Avatar = null;
                _repoUsuario.Modificacion(usuario);

                TempData["Mensaje"] = "Avatar eliminado correctamente.";
            }
            catch
            {
                TempData["Error"] = "No se pudo eliminar el avatar.";
            }

            return RedirectToAction(nameof(Perfil));
        }

        // ===================== HELPERS =====================

        private async Task RefrescarSesionAsync(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        private Usuario? ObtenerUsuarioActual()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out int id)) return null;
            return _repoUsuario.ObtenerPorId(id);
        }

        private string? GuardarAvatar(IFormFile archivo)
        {
            var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();
            if (!ExtensionesPermitidas.Contains(extension) || archivo.Length > TamanioMaximoBytes)
                return null;

            var nombreArchivo = $"avatar_{Guid.NewGuid()}{extension}";
            var carpeta = Path.Combine(_entorno.WebRootPath, "img", "avatars");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            var rutaFisica = Path.Combine(carpeta, nombreArchivo);
            using (var stream = new FileStream(rutaFisica, FileMode.Create))
            {
                archivo.CopyTo(stream);
            }

            return $"/img/avatars/{nombreArchivo}";
        }

        private void EliminarArchivoSiExiste(string? rutaRelativa)
        {
            if (string.IsNullOrEmpty(rutaRelativa)) return;
            if (!rutaRelativa.StartsWith("/img/avatars/", StringComparison.OrdinalIgnoreCase)) return;

            var rutaFisica = Path.Combine(_entorno.WebRootPath, rutaRelativa.TrimStart('/'));
            if (System.IO.File.Exists(rutaFisica))
            {
                System.IO.File.Delete(rutaFisica);
            }
        }
    }
}
