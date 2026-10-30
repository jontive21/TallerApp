using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerApp.Web.Data;
using TallerApp.Web.Models;

namespace TallerApp.Web.Controllers
{
    [Authorize]
    public class RepuestosController : Controller
    {
        private readonly IRepositorioRepuesto _repositorioRepuesto;

        public RepuestosController(IRepositorioRepuesto repositorioRepuesto)
        {
            _repositorioRepuesto = repositorioRepuesto;
        }

        // GET: Repuestos
        public IActionResult Index()
        {
            var repuestos = _repositorioRepuesto.ObtenerTodos();
            return View(repuestos);
        }

        // GET: Repuestos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Repuestos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                _repositorioRepuesto.Alta(repuesto);
                return RedirectToAction(nameof(Index));
            }
            return View(repuesto);
        }

        // GET: Repuestos/Edit/5
        public IActionResult Edit(int id)
        {
            var repuesto = _repositorioRepuesto.ObtenerPorId(id);
            if (repuesto == null)
            {
                return NotFound();
            }
            return View(repuesto);
        }

        // POST: Repuestos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Repuesto repuesto)
        {
            if (id != repuesto.IdRepuesto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repositorioRepuesto.Modificar(repuesto);
                return RedirectToAction(nameof(Index));
            }
            return View(repuesto);
        }

        // GET: Repuestos/Delete/5
        public IActionResult Delete(int id)
        {
            var repuesto = _repositorioRepuesto.ObtenerPorId(id);
            if (repuesto == null)
            {
                return NotFound();
            }
            return View(repuesto);
        }

        // POST: Repuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repositorioRepuesto.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
