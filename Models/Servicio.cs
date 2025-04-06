using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Servicio
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        public string? Descripcion { get; set; }

        [DisplayName("Precio")]
        [Required]
        public decimal Precio { get; set; }
    }
}
