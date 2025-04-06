using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Cita
    {
        [Key]
        public Guid IdCita { get; set; }

        [DisplayName("Fecha y Hora")]
        [Required]
        public DateTime FechaHora { get; set; }

        [DisplayName("Motivo")]
        [Required]
        public string Motivo { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; } = "Pendiente";

        [ForeignKey("Mascota")]
        public Guid IdMascota { get; set; }
        public Mascota? Mascota { get; set; }

        [ForeignKey("Veterinario")]
        public Guid IdVeterinario { get; set; }
    }
}
