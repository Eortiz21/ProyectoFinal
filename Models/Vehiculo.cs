using PoyectoParqueo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoParqueo.Models
{
    public class Vehiculo
    {
        [Key]
        public string NoPlaca { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public string TipoVehiculo { get; set; }

        [ForeignKey(nameof(Cliente))]
        public int Id_Cliente { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}