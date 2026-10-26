namespace TallerApp.Web.Models
{
    public class Repuesto
    {
        public int IdRepuesto { get; set; }
        public string Nombre { get; set; } = "";
        public string Codigo { get; set; } = "";
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Foto { get; set; } = "";
    }
}
