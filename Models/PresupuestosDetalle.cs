namespace Tp8.Models
{
    public class PresupuestosDetalle
    {
       public Productos? producto { get; set; }
        public int cantidad { get; set; }

        public PresupuestosDetalle() { }
        public PresupuestosDetalle(Productos produ, int cant)
        {
            producto = produ;
            cantidad = cant;
        }


        public int Subtotal()
        {
            return cantidad * producto.precio;
        }
    }
}