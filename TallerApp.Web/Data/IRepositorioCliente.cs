using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioCliente
    {
        List<Cliente> ObtenerTodos(int pagina = 1, int registrosPorPagina = 10, string? busqueda = null);
        int ObtenerTotalClientes(string? busqueda = null);
        Cliente? ObtenerPorId(int id);
        void Alta(Cliente cliente);
        void Modificar(Cliente cliente);
        void Eliminar(int id);
    }
}