using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioOrdenTrabajo
    {
        // Devuelve el ID de la nueva orden creada
        int CrearOrdenConRepuestos(OrdenTrabajo orden, List<OrdenRepuesto> detalles);
    }
}