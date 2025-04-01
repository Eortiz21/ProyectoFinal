using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class Municipio
    {
        [Key]
        public Guid MunicipioId { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [DisplayName("Codigo de Municipio")]
        public int Codigo { get; set; }

        [ScaffoldColumn(false)]
        public bool Inactivo { get; set; }
        // Llave foránea
        [ForeignKey("Departamento")]
        public Guid DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }

    }
}