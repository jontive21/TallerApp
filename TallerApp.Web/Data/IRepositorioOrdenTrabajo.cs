using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioOrdenTrabajo
    {
        int CrearOrdenConRepuestos(OrdenTrabajo orden, List<OrdenRepuesto> detalles);
        List<OrdenTrabajo> ObtenerTodos();
        void Eliminar(int id);
    }
}