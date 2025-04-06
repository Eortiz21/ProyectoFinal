using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Compra
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Proveedor")]
        [ForeignKey("Proveedor")]
        public Guid ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }

        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [DisplayName("Total")]
        public decimal Total { get; set; }

        [DisplayName("Usuario (creador)")]
        public string? UsuarioId { get; set; }
    }
}
