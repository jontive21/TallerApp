using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerApp.Web.Data;
using TallerApp.Web.Models;

namespace TallerApp.Web.Controllers
{
    [Authorize]
    public class MecanicosController : Controller
    {
        private readonly IRepositorioMecanico _repositorioMecanico;

        public MecanicosController(IRepositorioMecanico repositorioMecanico)
        {
            _repositorioMecanico = repositorioMecanico;
        }

        // GET: Mecanicos
        public IActionResult Index(string? busqueda, int pagina = 1)
        {
            int registrosPorPagina = 10;
            var mecanicos = _repositorioMecanico.ObtenerTodos(pagina, registrosPorPagina, busqueda);
            int totalRegistros = _repositorioMecanico.ObtenerTotalMecanicos(busqueda);
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);

            ViewBag.Busqueda = busqueda;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

            return View(mecanicos);
        }

        // GET: Mecanicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mecanicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Mecanico mecanico)
        {
            if (ModelState.IsValid)
            {
                _repositorioMecanico.Alta(mecanico);
                return RedirectToAction(nameof(Index));
            }
            return View(mecanico);
        }

        // GET: Mecanicos/Edit/5
        public IActionResult Edit(int id)
        {
            var mecanico = _repositorioMecanico.ObtenerPorId(id);
            if (mecanico == null)
            {
                return NotFound();
            }
            return View(mecanico);
        }

        // POST: Mecanicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Mecanico mecanico)
        {
            if (id != mecanico.IdMecanico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repositorioMecanico.Modificar(mecanico);
                return RedirectToAction(nameof(Index));
            }
            return View(mecanico);
        }

        // GET: Mecanicos/Delete/5
        public IActionResult Delete(int id)
        {
            var mecanico = _repositorioMecanico.ObtenerPorId(id);
            if (mecanico == null)
            {
                return NotFound();
            }
            return View(mecanico);
        }

        // POST: Mecanicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repositorioMecanico.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}