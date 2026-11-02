using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TallerApp.Web.Data;
using TallerApp.Web.Models;

namespace TallerApp.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly IRepositorioUsuario _repoUsuario;

        public ApiAuthController(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            // 1. Buscar el usuario
            var usuario = _repoUsuario.ObtenerPorEmail(model.Email);

            // 2. Verificar que exista y la contraseña sea correcta
            if (usuario == null || !PasswordHelper.VerificarHash(model.Password, usuario.PasswordHash))
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas" });
            }

            // 3. Crear los Claims (la información que irá dentro del token)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            // 4. Configurar la clave secreta (debe ser la misma que en Program.cs)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TuClaveSecretaMuyLargaYSeguraDeAlMenos32Caracteres123!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 5. Generar el Token
            var token = new JwtSecurityToken(
                issuer: "TallerAppIssuer",
                audience: "TallerAppAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(2), // El token dura 2 horas
                signingCredentials: creds
            );

            // 6. Devolver el token al cliente
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                usuario = usuario.NombreCompleto,
                rol = usuario.Rol
            });
        }
    }
}