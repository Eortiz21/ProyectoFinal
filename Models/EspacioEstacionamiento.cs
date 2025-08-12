using PoyectoParqueo.Models;
using System.ComponentModel.DataAnnotations;

namespace ProyectoParqueo.Models
{
    public class EspacioEstacionamiento
    {
        [Key]
        public int Id_Espacio { get; set; }
        public string No_Espacio { get; set; }
        public string Nivel { get; set; }
        public string TipoEspacio { get; set; }
        public string Estado { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}