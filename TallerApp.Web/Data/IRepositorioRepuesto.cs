using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioRepuesto
    {
        List<Repuesto> ObtenerTodos();
        Repuesto? ObtenerPorId(int id);
        void Alta(Repuesto repuesto);
        void Modificar(Repuesto repuesto);
        void Eliminar(int id);
    }
}
