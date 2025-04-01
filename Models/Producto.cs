using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Producto
    {
        [Key]
        public Guid IdProducto { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        public string? Descripcion { get; set; }

        [DisplayName("Categoría")]
        [Required]
        public string Categoria { get; set; }

        [DisplayName("Precio")]
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [DisplayName("Stock")]
        [Required]
        public int Stock { get; set; } = 0;

        [DisplayName("Lote")]
        public string? Lote { get; set; }

        [DisplayName("Fecha de Vencimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }

        // Llave foránea corregida
        [ForeignKey("Proveedor")]
        public Guid? idProveedor { get; set; }  // Ahora coincide con el tipo en Proveedor
        public Proveedor? Proveedor { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; } = "Disponible";

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
