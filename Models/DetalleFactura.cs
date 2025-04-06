using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class DetalleFactura
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Factura")]
        public Guid FacturaId { get; set; }
        public Factura? Factura { get; set; }

        [ForeignKey("Servicio")]
        public Guid ServicioId { get; set; }
        public Servicio? Servicio { get; set; }

        [DisplayName("Precio")]
        public decimal Precio { get; set; }
    }
}
