using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioMecanico
    {
        List<Mecanico> ObtenerTodos(int pagina = 1, int registrosPorPagina = 10, string? busqueda = null);
        int ObtenerTotalMecanicos(string? busqueda = null);
        Mecanico? ObtenerPorId(int id);
        void Alta(Mecanico mecanico);
        void Modificar(Mecanico mecanico);
        void Eliminar(int id);
    }
}