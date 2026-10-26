using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioMecanico
    {
        List<Mecanico> ObtenerTodos();
        Mecanico? ObtenerPorId(int id);
        void Alta(Mecanico mecanico);
        void Modificar(Mecanico mecanico);
        void Eliminar(int id);
    }
}
