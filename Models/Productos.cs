namespace Tp7.Models
{
    public class Productos 
    {
       public int idProducto { get; set; }
       public string? descripcion { get; set; }
       public int precio { get; set; }

        public Productos()
        {}
        public Productos(int id , string desc,int pre)
        {
            idProducto = id;
            descripcion = desc;
            precio = pre;
        }

        
    }
    
}