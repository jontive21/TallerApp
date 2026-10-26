namespace TallerApp.Web.Models
{
    public class ComprobantePago
    {
        public int IdComprobante { get; set; }
        public int OrdenId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string ArchivoComprobante { get; set; } = "";
        public string Estado { get; set; } = "Pendiente";
        public int IdUsuarioCreacion { get; set; }
    }
}
