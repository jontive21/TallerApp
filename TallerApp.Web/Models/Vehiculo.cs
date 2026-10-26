namespace TallerApp.Web.Models
{
    public class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public string Patente { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int Anio { get; set; }
        public string Color { get; set; } = "";
        public int ClienteId { get; set; }
    }
}
