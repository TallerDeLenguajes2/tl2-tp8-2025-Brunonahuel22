using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tp8.ViewModels
{
    public class AgregarProductoViewModel
    {
        public int IdPresupuesto { get; set; }

        [Display(Name = "Producto a agregar")]
        public int IdProducto { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Cantidad { get; set; }

        
        public SelectList ListaProductos { get; set; }
    }
}
