using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TallerApp.Web.Data;
using TallerApp.Web.Models;

namespace TallerApp.Web.Controllers
{
    [Authorize]
    public class VehiculosController : Controller
    {
        private readonly IRepositorioVehiculo _repositorioVehiculo;
        private readonly IRepositorioCliente _repositorioCliente;

        public VehiculosController(IRepositorioVehiculo repositorioVehiculo, IRepositorioCliente repositorioCliente)
        {
            _repositorioVehiculo = repositorioVehiculo;
            _repositorioCliente = repositorioCliente;
        }

        // GET: Vehiculos
        public IActionResult Index(string? busqueda, int pagina = 1)
        {
            int registrosPorPagina = 10;
            var vehiculos = _repositorioVehiculo.ObtenerTodos(pagina, registrosPorPagina, busqueda);
            int totalRegistros = _repositorioVehiculo.ObtenerTotalVehiculos(busqueda);
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);

            ViewBag.Busqueda = busqueda;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Clientes = _repositorioCliente.ObtenerTodos(1, int.MaxValue);
            
            return View(vehiculos);
        }

        // GET: Vehiculos/Create
        public IActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_repositorioCliente.ObtenerTodos(), "IdCliente", "NombreCompleto");
            return View();
        }

        // POST: Vehiculos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                _repositorioVehiculo.Alta(vehiculo);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClienteId = new SelectList(_repositorioCliente.ObtenerTodos(), "IdCliente", "NombreCompleto", vehiculo.ClienteId);
            return View(vehiculo);
        }

        // GET: Vehiculos/Edit/5
        public IActionResult Edit(int id)
        {
            var vehiculo = _repositorioVehiculo.ObtenerPorId(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            ViewBag.ClienteId = new SelectList(_repositorioCliente.ObtenerTodos(), "IdCliente", "NombreCompleto", vehiculo.ClienteId);
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.IdVehiculo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repositorioVehiculo.Modificar(vehiculo);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClienteId = new SelectList(_repositorioCliente.ObtenerTodos(), "IdCliente", "NombreCompleto", vehiculo.ClienteId);
            return View(vehiculo);
        }

        // GET: Vehiculos/Delete/5
        public IActionResult Delete(int id)
        {
            var vehiculo = _repositorioVehiculo.ObtenerPorId(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            ViewBag.ClienteNombre = _repositorioCliente.ObtenerPorId(vehiculo.ClienteId)?.NombreCompleto;
            return View(vehiculo);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repositorioVehiculo.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}