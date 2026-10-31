using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioVehiculo
    {
        List<Vehiculo> ObtenerTodos(int pagina = 1, int registrosPorPagina = 10, string? busqueda = null);
        int ObtenerTotalVehiculos(string? busqueda = null);
        Vehiculo? ObtenerPorId(int id);
        void Alta(Vehiculo vehiculo);
        void Modificar(Vehiculo vehiculo);
        void Eliminar(int id);
    }
}