using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PoyectoParqueo.Models
{
    public class Pago
    {
        [Key]
        public int Id_Pago { get; set; }

        [ForeignKey(nameof(Ticket))]
        public int Id_Ticket { get; set; }
        public Ticket Ticket { get; set; }

        public decimal MontoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }
        public string EstadoPago { get; set; }
    }
}
