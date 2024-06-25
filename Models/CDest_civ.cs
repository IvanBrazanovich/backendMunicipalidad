using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMunicipalidadTP.Models
{

    [Table("CDest_civ")] // Especificar el nombre de la tabla correcta
    public class CDest_civ
    {
        [Key] public int Codigo { get; set; } // Clave primaria
        public string? Descripcion { get; set; }

        public ICollection<Propietario>? Propietarios { get; set; }
    }
}
