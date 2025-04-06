using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Mascota
    {
        [Key]
        public Guid IdMascota { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string Nombre { get; set; }

        [DisplayName("Especie")]
        [Required]
        public string Especie { get; set; }

        [DisplayName("Raza")]
        public string? Raza { get; set; }

        [DisplayName("Edad")]
        public int? Edad { get; set; }

        [DisplayName("Peso (Kg)")]
        public decimal? Peso { get; set; }

        [DisplayName("Sexo")]
        [Required]
        public string Sexo { get; set; }

        [DisplayName("Cliente")]
        [ForeignKey("Cliente")]
        public Guid IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
