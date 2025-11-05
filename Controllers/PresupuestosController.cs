
using Microsoft.AspNetCore.Mvc;
using Tp8.Models;
using Tp8.Repository;
namespace Tp8.Controllers;

public class PresupuestosController : Controller
{
    private PresupuestosRepository _presupuestoRepository;

    public PresupuestosController()
    {
        _presupuestoRepository = new PresupuestosRepository();
    }


    // traer las listas de presupuestos 
    [HttpGet]
    public IActionResult Index()
    {

        List<Presupuestos> lista = _presupuestoRepository.ListarPresupuestos();
        return View(lista);
    }
}
