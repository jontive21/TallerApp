using TallerApp.Web.Models;
using System.Collections.Generic;

namespace TallerApp.Web.Data
{
    public interface IRepositorioComprobantePago
    {
        List<ComprobantePago> ObtenerTodos(int pagina = 1, int registrosPorPagina = 10, string? busqueda = null);
        int ObtenerTotalComprobantes(string? busqueda = null);
        ComprobantePago? ObtenerPorId(int id);
        void Alta(ComprobantePago comprobante);
        void Modificar(ComprobantePago comprobante);
        void Eliminar(int id);
    }
}