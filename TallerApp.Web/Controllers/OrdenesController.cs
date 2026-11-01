using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerApp.Web.Data;
using TallerApp.Web.Models;

namespace TallerApp.Web.Controllers
{
    [Authorize]
    public class OrdenesController : Controller
    {
        private readonly IRepositorioCliente _repoCliente;
        private readonly IRepositorioVehiculo _repoVehiculo;
        private readonly IRepositorioMecanico _repoMecanico;
        private readonly IRepositorioRepuesto _repoRepuesto;

        public OrdenesController(
            IRepositorioCliente repoCliente,
            IRepositorioVehiculo repoVehiculo,
            IRepositorioMecanico repoMecanico,
            IRepositorioRepuesto repoRepuesto)
        {
            _repoCliente = repoCliente;
            _repoVehiculo = repoVehiculo;
            _repoMecanico = repoMecanico;
            _repoRepuesto = repoRepuesto;
        }

        // GET: Ordenes/Create  → Esta es la vista con Vue.js
        public IActionResult Create()
        {
            return View();
        }

        // ========== Endpoints para Vue.js ==========

        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _repoCliente.ObtenerTodos(1, 1000); // traemos bastantes
            return Json(clientes);
        }

        [HttpGet]
        public IActionResult GetVehiculosPorCliente(int clienteId)
        {
            var vehiculos = _repoVehiculo.ObtenerPorCliente(clienteId);
            return Json(vehiculos);
        }

        [HttpGet]
        public IActionResult GetMecanicos()
        {
            var mecanicos = _repoMecanico.ObtenerTodos(1, 1000);
            return Json(mecanicos);
        }

        [HttpGet]
        public IActionResult GetRepuestos(string? busqueda = null)
        {
            var repuestos = _repoRepuesto.ObtenerTodos(1, 50, busqueda);
            return Json(repuestos);
        }
    }
}