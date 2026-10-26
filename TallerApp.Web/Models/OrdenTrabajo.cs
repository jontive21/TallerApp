namespace TallerApp.Web.Models
{
    public class OrdenTrabajo
    {
        public int IdOrden { get; set; }
        public int VehiculoId { get; set; }
        public int MecanicoId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEntregaEstimada { get; set; }
        public DateTime? FechaEntregaReal { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string DescripcionProblema { get; set; } = "";
        public string Observaciones { get; set; } = "";
        public int IdUsuarioCreacion { get; set; }
    }
}
