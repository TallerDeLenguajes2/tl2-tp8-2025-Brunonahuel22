using Tp8.Models;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using Tp8.ViewModels;
using Tp8.Interfaces;
namespace Tp8.Repository;




public class ProductoRepository : IProductoRepository
{
    string cadenaConexion = "Data Source=tienda.db";
    public Productos CrearProducto(Productos producto)
    {

        using SqliteConnection connection = new SqliteConnection(cadenaConexion);

        connection.Open();

        string sql = "INSERT INTO Productos (descripcion, precio) VALUES (@descripcion, @precio)";
        using SqliteCommand comando = new SqliteCommand(sql, connection);

        comando.Parameters.Add(new SqliteParameter("@descripcion", producto.descripcion));
        comando.Parameters.Add(new SqliteParameter("@precio", producto.precio));


        comando.ExecuteNonQuery();



        return producto;

    }

    //Listar todos los Productos registrados. (devuelve un List de Producto)
    public List<Productos> ListarProductos()
    {
        var lista = new List<Productos>();
        using var connection = new SqliteConnection(cadenaConexion);
        connection.Open();
        string sql = "SELECT * FROM Productos";

        using SqliteCommand comando = new SqliteCommand(sql, connection);

        using var lectura = comando.ExecuteReader();

        while (lectura.Read())
        {
            var p = new Productos
            {
                idProducto = Convert.ToInt32(lectura["idProducto"]),
                descripcion = lectura["descripcion"].ToString(),
                precio = Convert.ToInt32(lectura["precio"])

            };

            lista.Add(p);
        }

        return lista;
    }


    //Obtener detalles de un Productos por su ID
    public Productos? ObtenerPorId(int id)
    {
        using SqliteConnection connection = new SqliteConnection(cadenaConexion);
        connection.Open();

        string sql = "SELECT * FROM Productos WHERE idProducto = @id";

        using SqliteCommand command = new SqliteCommand(sql, connection);

        // despues de comando le puedo sacar el id por parametro
        command.Parameters.Add(new SqliteParameter("@id", id));



        // comando ya no se crea como objeto , sino de lo usa comman.
        using var lector = command.ExecuteReader();

        if (lector.Read())
        {
            var p = new Productos
            {
                idProducto = Convert.ToInt32(lector["idProducto"]),
                descripcion = lector["descripcion"].ToString(),
                precio = Convert.ToInt32(lector["precio"])
            };

            return p;
        }

        return null;
    }

    //Modificar un Producto existente. (recibe un Id y un objeto Producto)
    public int ModificarProducto(Productos producto)
    {
        using var connection = new SqliteConnection(cadenaConexion);
        connection.Open();

        string sql = "UPDATE Productos SET descripcion = @descripcion, precio = @precio WHERE idProducto = @id";

        using var command = new SqliteCommand(sql, connection);

        command.Parameters.Add(new SqliteParameter("@id", producto.idProducto));
        command.Parameters.Add(new SqliteParameter("@descripcion", producto.descripcion));
        command.Parameters.Add(new SqliteParameter("@precio", producto.precio));

        return command.ExecuteNonQuery();
    }


    //Eliminar un Producto por ID
    public int EliminarProducto(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = "DELETE FROM Productos WHERE idProducto = @Id";

        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));

        int filasAfectadas = comando.ExecuteNonQuery();

        return filasAfectadas;
    }




}
