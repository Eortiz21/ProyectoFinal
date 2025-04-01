using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace laboratorio1ElvisOrtiz160625.Models

{
    public class Departamento

    {
        [Key]
        public Guid DepartamentoId { get; set; }

        [DisplayName("Nombre de departamento")]
        public string? Nombre { get; set; }

        [DisplayName("codigo de Departamento")]
        public int Codigo { get; set; }

        [ScaffoldColumn(false)]
        public bool Inactivo { get; set; }
    }
}