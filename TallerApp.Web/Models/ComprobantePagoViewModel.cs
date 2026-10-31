using System.ComponentModel.DataAnnotations;

namespace TallerApp.Web.Models
{
    public class ComprobantePagoViewModel
    {
        public int IdComprobante { get; set; }

        [Required(ErrorMessage = "La orden es obligatoria.")]
        [Display(Name = "Orden N°")]
        public int OrdenId { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Pago")]
        public DateTime FechaPago { get; set; }

        [Display(Name = "Archivo (PDF, JPG, PNG)")]
        public IFormFile? Archivo { get; set; }

        public string? ArchivoComprobante { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "Pendiente";
    }
}