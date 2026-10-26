namespace TallerApp.Web.Models
{
    public class Mecanico
    {
        public int IdMecanico { get; set; }
        public string NombreCompleto { get; set; } = "";
        public string Matricula { get; set; } = "";
        public string Especialidad { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefono { get; set; } = "";
    }
}
