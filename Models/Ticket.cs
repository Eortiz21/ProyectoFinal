using ProyectoParqueo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PoyectoParqueo.Models
{
    public class Ticket
    {
        [Key]
        public int Id_Ticket { get; set; }

        [ForeignKey(nameof(Vehiculo))]
        public string NoPlaca { get; set; }
        public Vehiculo Vehiculo { get; set; }

        [ForeignKey(nameof(EspacioEstacionamiento))]
        public int Id_Espacio { get; set; }
        public EspacioEstacionamiento EspacioEstacionamiento { get; set; }

        public DateTime Fecha_hora_entrada { get; set; }
        public DateTime? Fecha_hora_salida { get; set; }

        [ForeignKey(nameof(Tarifa))]
        public int Id_Tarifa { get; set; }
        public Tarifa Tarifa { get; set; }

        public decimal PagoTotal { get; set; }

        public ICollection<Pago> Pagos { get; set; }
    }

}
