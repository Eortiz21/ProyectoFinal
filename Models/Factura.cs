using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Factura
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Cliente")]
        [ForeignKey("Cliente")]
        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [DisplayName("Total")]
        public decimal Total { get; set; }

       
    }
}
