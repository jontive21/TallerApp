namespace TallerApp.Web.Models
{
    public class OrdenRepuesto
    {
        public int IdOrdenRepuesto { get; set; }
        public int OrdenId { get; set; }
        public int RepuestoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
