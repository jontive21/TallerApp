using TallerApp.Web.Models;
using System.Collections.Generic;

   namespace TallerApp.Web.Data
{
    public interface IRepositorioUsuario
    {
        List<Usuario> ObtenerTodos();
        Usuario? ObtenerPorEmail(string email);
        Usuario? ObtenerPorId(int id);
        void Alta(Usuario usuario);
        void Modificacion(Usuario usuario);
        void Baja(int id);
    }
}