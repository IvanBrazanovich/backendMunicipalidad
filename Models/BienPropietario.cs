using System.ComponentModel.DataAnnotations.Schema;

namespace WebMunicipalidadTP.Models
{
    [Table("BienPropietario")] // Especificar el nombre de la tabla correcta
    public class BienPropietario
    {
        [ForeignKey("Bien")]
        public int Id_Bien { get; set; } // Clave primaria compuesta

        [ForeignKey("Propietario")]
        public int Id_Propietario { get; set; } // Clave primaria compuesta

        public decimal? Porcentaje_Propiedad { get; set; }

        public required Bien Bien { get; set; }
        public required Propietario Propietario { get; set; }
    }
}
