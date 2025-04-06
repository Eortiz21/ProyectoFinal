using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class DetalleCompra
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Compra")]
        public Guid CompraId { get; set; }
        public Compra? Compra { get; set; }

        [ForeignKey("Producto")]
        public Guid ProductoId { get; set; }
        public Producto? Producto { get; set; }

        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }

        [DisplayName("Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [DisplayName("Subtotal")]
        public decimal Subtotal { get; set; }
    }
}
