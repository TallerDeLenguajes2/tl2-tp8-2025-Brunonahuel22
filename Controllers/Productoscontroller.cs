using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp8.Models;

using Tp8.Repository;

namespace Tp8.Controllers;

public class ProductosController : Controller
{
    private ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }



    //A partir de aquí van todos los Action Methods (Get, Post,etc.)

    //Ejemplo de cómo “Listar” los producto desde Index
    [HttpGet]
    public IActionResult Index()
    {
        List<Productos> productos = productoRepository.listarProductos();
        return View(productos);
    }
}