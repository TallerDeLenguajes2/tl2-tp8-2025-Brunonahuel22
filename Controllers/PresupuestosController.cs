using Microsoft.AspNetCore.Mvc;
using Tp8.Models;
using Tp8.Repository;
using Tp8.ViewModels;

using Microsoft.AspNetCore.Mvc.Rendering;
using MiProyectoDI.Interfaces; // Necesario para SelectList

namespace Tp8.Controllers
{
    public class PresupuestosController : Controller
    {
        private IPresupuestoRepository _repo ;
        // Necesitamos el repositorio de Productos para llenar el dropdown
        private readonly ProductoRepository _productoRepo = new ProductoRepository();

        public PresupuestosController( IPresupuestoRepository repo)
        {
            _repo = repo;            
        }

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
        public IActionResult Create(PresupuestoViewModel nuevo)
        {


            if (!ModelState.IsValid)
            {

                return View(nuevo);
            }

            var nuevoPresupuesto = new Presupuestos
            {
                nombreDestinatario = nuevo.NombreDestinatario,
                FechaCreacion = nuevo.FechaCreacion

            };


            _repo.CrearPresupuestos(nuevoPresupuesto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Presupuestos/AgregarProducto
        [HttpGet]
        public IActionResult AgregarProducto(int id)
        {
            // 1. Obtener los productos para el SelectList
            List<Productos> productos = _productoRepo.ListarProductos();





            // 2. Crear el ViewModel
            AgregarProductoViewModel model = new AgregarProductoViewModel
            {
                IdPresupuesto = id, // Pasamos el ID del presupuesto actual
                                    // 3. Crear el SelectList
                ListaProductos = new SelectList(productos, "idProducto", "descripcion")
            };

            ViewBag.MiLista = model.ListaProductos;
            return View(model);
        }

        [HttpPost]
        public IActionResult AgregarProducto(AgregarProductoViewModel model)
        {
            // 1. Chequeo de Seguridad para la Cantidad
            if (!ModelState.IsValid)
            {
                // LÓGICA CRÍTICA DE RECARGA: Si falla la validación,
                // debemos recargar el SelectList porque se pierde en el POST.
                var productos = _productoRepo.ListarProductos();
                model.ListaProductos = new SelectList(productos, "idProducto", "descripcion");

                // Devolvemos el modelo con los errores y el dropdown recargado
                return View(model);
            }

            // 2. Si es VÁLIDO: Llamamos al repositorio para guardar la relación
            _repo.AgregarProductoAPresupuesto(model.IdPresupuesto, model.IdProducto, model.Cantidad);

            // 3. Redirigimos al detalle del presupuesto
            return RedirectToAction(nameof(PresupuestosDetalle), new { id = model.IdPresupuesto });
        }








        // ---------- EDIT ----------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var presupuesto = _repo.ObtenerPresupuestoPorId(id);
            return View(presupuesto);
        }

        [HttpPost]
        public IActionResult Edit(PresupuestoViewModel editado)
        {
            if (!ModelState.IsValid)
            {
                return View(editado);
            }

            var presupuestoEditado = new Presupuestos
            {
                nombreDestinatario = editado.NombreDestinatario,
                FechaCreacion = editado.FechaCreacion
            };




            _repo.EditarPresupuesto(presupuestoEditado);
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
