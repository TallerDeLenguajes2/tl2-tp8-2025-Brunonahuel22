using System.Collections.Generic;
using Tp8.Models;

namespace MiProyectoDI.Interfaces
{
    public interface IPresupuestoRepository
    {
        Presupuestos CrearPresupuestos(Presupuestos p);
        Presupuestos ObtenerPresupuestoPorId(int id);
        List<Presupuestos> ListarPresupuestos();
        int EditarPresupuesto(Presupuestos p);
        int EliminarPresupuesto(int id);
          public void AgregarProductoAPresupuesto(int idPresupuesto, int idProducto, int cantidad);
    }
}


