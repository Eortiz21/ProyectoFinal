using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Proveedor
    {
        [Key]
        public Guid IdProveedor { get; set; } // Corregido el nombre

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [DisplayName("Teléfono")]
        public string? Telefono { get; set; }

        [DisplayName("Dirección")]
        public string? Direccion { get; set; }

        [DisplayName("Correo Electrónico")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
        public string? Correo { get; set; }

        // Relación con Productos
        public ICollection<Producto>? Productos { get; set; }
    }
}
