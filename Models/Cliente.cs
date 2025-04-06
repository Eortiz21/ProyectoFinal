using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Cliente
    {
        [Key]
        public Guid IdCliente { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string Nombre { get; set; }

        [DisplayName("Teléfono")]
        [Required]
        public string Telefono { get; set; }

        [DisplayName("Dirección")]
        public string? Direccion { get; set; }

        [DisplayName("Correo")]
        [EmailAddress]
        public string? Correo { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Mascota>? Mascotas { get; set; }
    }
}
