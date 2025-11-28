using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp8.Models;
using Tp8.ViewModels;


using Tp8.Repository;
using Tp8.Interfaces;

namespace Tp8.Controllers;

public class ProductosController : Controller
{
    private IProductoRepository productoRepository;
    public ProductosController(IProductoRepository pro)
    {
        productoRepository = pro;
    }



    //A partir de aquí van todos los Action Methods (Get, Post,etc.)

    //Ejemplo de cómo “Listar” los producto desde Index
    [HttpGet]
    public IActionResult Index()
    {
        List<Productos> productos = productoRepository.ListarProductos();
        return View(productos);
    }


    [HttpGet]

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]

    public IActionResult Create(ProductoViewModel p)
    {
        // 1. CHEQUEO DE SEGURIDAD DEL SERVIDOR
        if (!ModelState.IsValid)
        {
            // Si falla: Devolvemos el ViewModel con los datos y errores a la Vista
            return View(p);
        }

        // 2. SI ES VÁLIDO: Mapeo Manual de VM a Modelo de Dominio
        var nuevoProducto = new Productos
        {
            descripcion = p.Descripcion,
            precio = p.Precio
        };




        productoRepository.CrearProducto(nuevoProducto);
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = productoRepository.ObtenerPorId(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult Edit(ProductoViewModel productoVM, int id)
    {

        if (id != productoVM.IdProducto) return NotFound();

        // 1. CHEQUEO DE SEGURIDAD DEL SERVIDOR
        if (!ModelState.IsValid)
        {
            return View(productoVM);
        }

        // 2. Mapeo Manual de VM a Modelo de Dominio
        var productoAEditar = new Productos
        {
            idProducto = productoVM.IdProducto, // Necesario para el UPDATE
            descripcion = productoVM.Descripcion,
            precio = productoVM.Precio
        };




        productoRepository.ModificarProducto(productoAEditar);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult Delete(int id)
    {
        var producto = productoRepository.ObtenerPorId(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult DeleteConfirmado(int id)
    {
        productoRepository.EliminarProducto(id);
        return RedirectToAction("Index");
    }
}