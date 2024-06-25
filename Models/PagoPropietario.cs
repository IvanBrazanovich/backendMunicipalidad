using System.ComponentModel.DataAnnotations.Schema;

namespace WebMunicipalidadTP.Models
{
    [Table("PagoPropietario")] // Asegúrate de que el nombre de la tabla sea correcto
    public class PagoPropietario
    {
        public int Id_Pago { get; set; }
        public int Id_Propietario { get; set; }
        public int Id_Bien { get; set; }

        [ForeignKey("Id_Pago")]
        public Pago? Pago { get; set; }

        [ForeignKey("Id_Propietario")]
        public required Propietario Propietario { get; set; }

        [ForeignKey("Id_Bien")]
        public required Bien Bien { get; set; }
    }
}
