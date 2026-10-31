using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioRepuesto
    {
        List<Repuesto> ObtenerTodos(int pagina = 1, int registrosPorPagina = 10, string? busqueda = null);
        int ObtenerTotalRepuestos(string? busqueda = null);
        Repuesto? ObtenerPorId(int id);
        void Alta(Repuesto repuesto);
        void Modificar(Repuesto repuesto);
        void Eliminar(int id);
    }
}