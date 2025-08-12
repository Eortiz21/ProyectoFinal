using ProyectoParqueo.Models;
using System.ComponentModel.DataAnnotations;

namespace PoyectoParqueo.Models
{
    public class Cliente
    {
        [Key]
        public int Id_Cliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
