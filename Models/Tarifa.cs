using System.ComponentModel.DataAnnotations;

namespace PoyectoParqueo.Models
{
    public class Tarifa
    {
        [Key]
        public int Id_Tarifa { get; set; }
        public string TipoTarifa { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
