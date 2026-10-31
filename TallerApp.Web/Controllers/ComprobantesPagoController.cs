using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerApp.Web.Data;
using TallerApp.Web.Models;

namespace TallerApp.Web.Controllers
{
    [Authorize]
    public class ComprobantesPagoController : Controller
    {
        private readonly IRepositorioComprobantePago _repositorio;
        private readonly IWebHostEnvironment _entorno;

        private static readonly string[] ExtensionesPermitidas = { ".pdf", ".jpg", ".jpeg", ".png" };
        private const long TamanioMaximoBytes = 5 * 1024 * 1024; // 5 MB

        public ComprobantesPagoController(IRepositorioComprobantePago repositorio, IWebHostEnvironment entorno)
        {
            _repositorio = repositorio;
            _entorno = entorno;
        }

        // GET: ComprobantesPago
        public IActionResult Index(string? busqueda, int pagina = 1)
        {
            int registrosPorPagina = 10;
            var comprobantes = _repositorio.ObtenerTodos(pagina, registrosPorPagina, busqueda);
            int totalRegistros = _repositorio.ObtenerTotalComprobantes(busqueda);
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);

            ViewBag.Busqueda = busqueda;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

            return View(comprobantes);
        }

        // GET: ComprobantesPago/Create
        public IActionResult Create()
        {
            return View(new ComprobantePagoViewModel { FechaPago = DateTime.Today });
        }

        // POST: ComprobantesPago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComprobantePagoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string rutaArchivo = model.ArchivoComprobante ?? "";

                if (model.Archivo != null && model.Archivo.Length > 0)
                {
                    var resultado = await GuardarArchivo(model.Archivo);
                    if (resultado != null)
                    {
                        ModelState.AddModelError("Archivo", resultado);
                        return View(model);
                    }
                    rutaArchivo = await SubirArchivo(model.Archivo);
                }

                var comprobante = new ComprobantePago
                {
                    OrdenId = model.OrdenId,
                    Monto = model.Monto,
                    FechaPago = model.FechaPago,
                    ArchivoComprobante = rutaArchivo,
                    Estado = model.Estado,
                    IdUsuarioCreacion = 1
                };

                _repositorio.Alta(comprobante);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ComprobantesPago/Edit/5
        public IActionResult Edit(int id)
        {
            var comprobante = _repositorio.ObtenerPorId(id);
            if (comprobante == null) return NotFound();

            var model = new ComprobantePagoViewModel
            {
                IdComprobante = comprobante.IdComprobante,
                OrdenId = comprobante.OrdenId,
                Monto = comprobante.Monto,
                FechaPago = comprobante.FechaPago,
                ArchivoComprobante = comprobante.ArchivoComprobante,
                Estado = comprobante.Estado
            };
            return View(model);
        }

        // POST: ComprobantesPago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComprobantePagoViewModel model)
        {
            if (id != model.IdComprobante) return NotFound();

            if (ModelState.IsValid)
            {
                string rutaArchivo = model.ArchivoComprobante ?? "";

                if (model.Archivo != null && model.Archivo.Length > 0)
                {
                    if (!string.IsNullOrEmpty(model.ArchivoComprobante))
                    {
                        EliminarArchivoSiExiste(model.ArchivoComprobante);
                    }

                    var resultado = await GuardarArchivo(model.Archivo);
                    if (resultado != null)
                    {
                        ModelState.AddModelError("Archivo", resultado);
                        return View(model);
                    }
                    rutaArchivo = await SubirArchivo(model.Archivo);
                }

                var comprobante = new ComprobantePago
                {
                    IdComprobante = model.IdComprobante,
                    OrdenId = model.OrdenId,
                    Monto = model.Monto,
                    FechaPago = model.FechaPago,
                    ArchivoComprobante = rutaArchivo,
                    Estado = model.Estado,
                    IdUsuarioCreacion = 1
                };

                _repositorio.Modificar(comprobante);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ComprobantesPago/Delete/5
        public IActionResult Delete(int id)
        {
            var comprobante = _repositorio.ObtenerPorId(id);
            if (comprobante == null) return NotFound();
            return View(comprobante);
        }

        // POST: ComprobantesPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var comprobante = _repositorio.ObtenerPorId(id);
            if (comprobante != null && !string.IsNullOrEmpty(comprobante.ArchivoComprobante))
            {
                EliminarArchivoSiExiste(comprobante.ArchivoComprobante);
            }
            _repositorio.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string?> GuardarArchivo(IFormFile archivo)
        {
            var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();
            if (!ExtensionesPermitidas.Contains(extension))
                return "Solo se permiten archivos PDF, JPG o PNG.";

            if (archivo.Length > TamanioMaximoBytes)
                return "El archivo no puede superar los 5 MB.";

            return null;
        }

        private async Task<string> SubirArchivo(IFormFile archivo)
        {
            var uploadsFolder = Path.Combine(_entorno.WebRootPath, "uploads", "comprobantes");
            Directory.CreateDirectory(uploadsFolder);

            var nombreUnico = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";
            var rutaCompleta = Path.Combine(uploadsFolder, nombreUnico);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            return $"/uploads/comprobantes/{nombreUnico}";
        }

        private void EliminarArchivoSiExiste(string? rutaRelativa)
        {
            if (string.IsNullOrEmpty(rutaRelativa)) return;
            if (!rutaRelativa.StartsWith("/uploads/comprobantes/", StringComparison.OrdinalIgnoreCase)) return;

            var rutaFisica = Path.Combine(_entorno.WebRootPath, rutaRelativa.TrimStart('/'));
            if (System.IO.File.Exists(rutaFisica))
            {
                System.IO.File.Delete(rutaFisica);
            }
        }
    }
}