using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMunicipalidadTP.Models
{
    namespace MunicipalidadTPApi.Models
    {

        [Table("Cuota")] // Especificar el nombre de la tabla correcta

        public class Cuota
        {
            [Key] public int Idcuota { get; set; } // Clave primaria
            public int? Id_bien { get; set; }
            public decimal? Monto { get; set; }
            public DateTime? Fecha_vencimiento { get; set; }
            public int? CodEstado { get; set; }

            public Bien? Bien { get; set; }
            public CDCuotaestado? Estado { get; set; }
            public ICollection<Pago>? Pagos { get; set; }
        }
    }

}
