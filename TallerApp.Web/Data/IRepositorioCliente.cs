using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioCliente
    {
        List<Cliente> ObtenerTodos();
        Cliente? ObtenerPorId(int id);
        void Alta(Cliente cliente);
        void Modificar(Cliente cliente);
        void Eliminar(int id);
    }
}
