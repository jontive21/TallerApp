using System.Collections.Generic;

namespace TallerApp.Web.Models
{
    public class CrearOrdenViewModel
    {
        public int VehiculoId { get; set; }
        public int MecanicoId { get; set; }
        public string FechaIngreso { get; set; }
        public string? FechaEntregaEstimada { get; set; }
        public string DescripcionProblema { get; set; } = "";
        public string Observaciones { get; set; } = "";
        
        // Lista de repuestos que viene desde el array "items" de Vue.js
        public List<DetalleOrdenDto> Repuestos { get; set; } = new List<DetalleOrdenDto>();
    }

    public class DetalleOrdenDto
    {
        public int RepuestoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}