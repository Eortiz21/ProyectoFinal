using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class HistorialMedico
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Mascota")]
        [ForeignKey("Mascota")]
        public Guid MascotaId { get; set; }
        public Mascota? Mascota { get; set; }

        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Tratamiento")]
        public string Tratamiento { get; set; }

        
    }
}
