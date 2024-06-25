using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMunicipalidadTP.Models
{
    [Table("CDtipoBien")] // Especificar el nombre de la tabla correcta
    public class CDtipoBien
    {
        [Key] public int Cod_tipo { get; set; } // Clave primaria
        public string? Descripcion { get; set; }

        public ICollection<Bien>? Bienes { get; set; }
    }
}
