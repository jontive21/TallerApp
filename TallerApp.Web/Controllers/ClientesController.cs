using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerApp.Web.Data;
using TallerApp.Web.Models;

namespace TallerApp.Web.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly IRepositorioCliente _repositorioCliente;

        public ClientesController(IRepositorioCliente repositorioCliente)
        {
            _repositorioCliente = repositorioCliente;
        }

        // GET: Clientes
        public IActionResult Index(string? busqueda, int pagina = 1)
        {
            int registrosPorPagina = 10;
            var clientes = _repositorioCliente.ObtenerTodos(pagina, registrosPorPagina, busqueda);
            int totalRegistros = _repositorioCliente.ObtenerTotalClientes(busqueda);
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);

            ViewBag.Busqueda = busqueda;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

            return View(clientes);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repositorioCliente.Alta(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public IActionResult Edit(int id)
        {
            var cliente = _repositorioCliente.ObtenerPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repositorioCliente.Modificar(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public IActionResult Delete(int id)
        {
            var cliente = _repositorioCliente.ObtenerPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repositorioCliente.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}