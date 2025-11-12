using Microsoft.Data.Sqlite;
using Tp8.Models;

namespace Tp8.Repository
{
    public class PresupuestosRepository
    {
        private string cadenaConexion = "Data Source=tienda.db";

        // 🔹 Crear un nuevo presupuesto
        public Presupuestos CrearPresupuesto(Presupuestos p)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "INSERT INTO Presupuestos (nombreDestinatario, FechaCreacion) VALUES (@nombre, @fecha)";
            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", p.nombreDestinatario);
            comando.Parameters.AddWithValue("@fecha", p.FechaCreacion);

            comando.ExecuteNonQuery();

            // 🔹 Obtener el ID generado automáticamente
            string sqlId = "SELECT last_insert_rowid()";
            using var cmdId = new SqliteCommand(sqlId, conexion);
            long idGenerado = (long)cmdId.ExecuteScalar();

            p.idPresupuesto = (int)idGenerado;

            return p;
        }
        public void EditarPresupuesto(Presupuestos p)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "UPDATE Presupuestos SET nombreDestinatario=@nombre, FechaCreacion=@fecha WHERE idPresupuesto=@id";
            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", p.nombreDestinatario);
            comando.Parameters.AddWithValue("@fecha", p.FechaCreacion);
            comando.Parameters.AddWithValue("@id", p.idPresupuesto);

            comando.ExecuteNonQuery();
        }


    
        public List<Presupuestos> ListarPresupuestos()
        {
            List<Presupuestos> lista = new List<Presupuestos>();

            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            // Traemos todos los presupuestos
            string sql = "SELECT idPresupuesto, nombreDestinatario, FechaCreacion FROM Presupuestos";
            using var comando = new SqliteCommand(sql, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                var p = new Presupuestos
                {
                    idPresupuesto = Convert.ToInt32(lector["idPresupuesto"]),
                    nombreDestinatario = lector["nombreDestinatario"].ToString(),
                    FechaCreacion = DateTime.Parse(lector["FechaCreacion"].ToString()),
                };
                lista.Add(p);
            }

            return lista;
        }


        // 🔹 Obtener presupuesto por ID (con sus productos y cantidades)
        public Presupuestos? ObtenerPresupuestoPorId(int id)
        {
            Presupuestos? presupuesto = null;
            List<PresupuestosDetalle> detalles = new List<PresupuestosDetalle>();

            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            // Datos del presupuesto principal
            string sqlPres = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";
            using var comandoPres = new SqliteCommand(sqlPres, conexion);
            comandoPres.Parameters.AddWithValue("@id", id);

            using var lectorPres = comandoPres.ExecuteReader();

            if (lectorPres.Read())
            {
                presupuesto = new Presupuestos
                {
                    idPresupuesto = Convert.ToInt32(lectorPres["idPresupuesto"]),
                    nombreDestinatario = lectorPres["nombreDestinatario"].ToString(),
                    FechaCreacion = DateTime.Parse(lectorPres["FechaCreacion"].ToString()),
                    Detalle = new List<PresupuestosDetalle>()
                };
            }
            lectorPres.Close();

            if (presupuesto != null)
            {
                string sqlDetalle = @"
            SELECT d.idProducto, d.cantidad, p.descripcion, p.precio
            FROM PresupuestoDetalles d
            JOIN Productos p ON p.idProducto = d.idProducto
            WHERE d.idPresupuesto = @idPresupuesto";

                using var comandoDet = new SqliteCommand(sqlDetalle, conexion);
                comandoDet.Parameters.AddWithValue("@idPresupuesto", presupuesto.idPresupuesto);

                using var lectorDet = comandoDet.ExecuteReader();
                while (lectorDet.Read())
                {
                    var producto = new Productos
                    {
                        idProducto = Convert.ToInt32(lectorDet["idProducto"]),
                        descripcion = lectorDet["descripcion"].ToString(),
                        precio = Convert.ToInt32(lectorDet["precio"])
                    };

                    var detalle = new PresupuestosDetalle
                    {
                        producto = producto,
                        cantidad = Convert.ToInt32(lectorDet["cantidad"])
                    };

                    presupuesto.Detalle.Add(detalle);
                }

            }

            return presupuesto;
        }

        // 🔹 Agregar producto y cantidad a un presupuesto existente
        public void AgregarProductoAPresupuesto(int idPresupuesto, int idProducto, int cantidad)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, cantidad) VALUES (@pres, @prod, @cant)";
            using var comando = new SqliteCommand(sql, conexion);

            comando.Parameters.AddWithValue("@pres", idPresupuesto);
            comando.Parameters.AddWithValue("@prod", idProducto);
            comando.Parameters.AddWithValue("@cant", cantidad);

            comando.ExecuteNonQuery();
        }

        // 🔹 Eliminar presupuesto (y sus detalles)
        public int EliminarPresupuesto(int id)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            // Primero borramos los detalles
            string sqlDetalles = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";
            using var cmdDet = new SqliteCommand(sqlDetalles, conexion);
            cmdDet.Parameters.AddWithValue("@id", id);
            cmdDet.ExecuteNonQuery();

            // Luego el presupuesto principal
            string sqlPres = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
            using var cmdPres = new SqliteCommand(sqlPres, conexion);
            cmdPres.Parameters.AddWithValue("@id", id);
            int filas = cmdPres.ExecuteNonQuery();

            return filas;
        }
    }
}
