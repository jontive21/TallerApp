using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TallerApp.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    // Le decimos que SOLO este controlador exija el Token JWT (Bearer)
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class ApiOrdenesController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            var nombreUsuario = User.Identity?.Name;
            return Ok(new 
            { 
                mensaje = $"¡Acceso concedido, {nombreUsuario}! Tu token JWT es válido.",
                nota = "Este endpoint está protegido por JWT."
            });
        }
    }
}