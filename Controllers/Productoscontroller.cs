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


    [HttpGet]

    public IActionResult Create()
    {  
        return View();
    }
    [HttpPost]

    public IActionResult Create(Productos p)
    {
        productoRepository.crearProducto(p);
        return RedirectToAction("Index");
    }


     [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = productoRepository.obtenerDetallePorId(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult Edit(Productos editado)
    {
        productoRepository.ModificarProductoExistente(editado.idProducto, editado);
        return RedirectToAction("Index");
    }
    

      [HttpGet]
    public IActionResult Delete(int id)
    {
        var producto = productoRepository.obtenerDetallePorId(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult DeleteConfirmado(int id)
    {
        productoRepository.EliminarProducto(id);
        return RedirectToAction("Index");
    }
}