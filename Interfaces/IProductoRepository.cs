using Tp8.Models;


namespace Tp8.Interfaces
{
    public interface IProductoRepository
    {
        // Crear producto
        Productos CrearProducto(Productos producto);

        // Obtener todos los productos
        List<Productos> ListarProductos();

        // Obtener un producto por ID
        Productos? ObtenerPorId(int id);



        // Actualizar producto
        int ModificarProducto(Productos producto);

        // Eliminar producto
        int EliminarProducto(int id);
    }
}