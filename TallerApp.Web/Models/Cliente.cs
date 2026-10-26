namespace TallerApp.Web.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NombreCompleto { get; set; } = "";
        public string DNI { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Email { get; set; } = "";
        public string Direccion { get; set; } = "";
    }
}
