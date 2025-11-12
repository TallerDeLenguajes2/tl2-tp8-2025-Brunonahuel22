using Microsoft.AspNetCore.Mvc;
using Tp8.Models;
using Tp8.Repository;

namespace Tp8.Controllers
{
    public class PresupuestosController : Controller
    {
        private PresupuestosRepository _repo = new PresupuestosRepository();

        // ---------- INDEX ----------
        [HttpGet]
        public IActionResult Index()
        {
            var lista = _repo.ListarPresupuestos();
            return View(lista);
        }

        // ---------- CREATE ----------
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Presupuestos nuevo)
        {
            if (ModelState.IsValid)
            {
                _repo.CrearPresupuesto(nuevo);
                return RedirectToAction("Index");
            }
            return View(nuevo);
        }

        // ---------- EDIT ----------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var presupuesto = _repo.ObtenerPresupuestoPorId(id);
            return View(presupuesto);
        }

        [HttpPost]
        public IActionResult Edit(Presupuestos editado)
        {
            _repo.EditarPresupuesto(editado); 
            return RedirectToAction("Index");
        }

        // ---------- DELETE ----------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var presupuesto = _repo.ObtenerPresupuestoPorId(id);
            return View(presupuesto);
        }

        [HttpPost]
        public IActionResult DeleteConfirmado(int id)
        {
            _repo.EliminarPresupuesto(id);
            return RedirectToAction("Index");
        }
    }
}
