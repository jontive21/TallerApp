using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioVehiculo
    {
        List<Vehiculo> ObtenerTodos();
        List<Vehiculo> ObtenerPorCliente(int clienteId);
        Vehiculo? ObtenerPorId(int id);
        void Alta(Vehiculo vehiculo);
        void Modificar(Vehiculo vehiculo);
        void Eliminar(int id);
    }
}
