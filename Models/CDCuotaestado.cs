using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMunicipalidadTP.Models.MunicipalidadTPApi.Models;

namespace WebMunicipalidadTP.Models
{
    [Table("CDCuotaestado")] // Especificar el nombre de la tabla correcta
    public class CDCuotaestado
    {
        [Key] public int Id_estado { get; set; } // Clave primaria
        public string? Descripcion { get; set; }

        public ICollection<Cuota>? Cuotas { get; set; }
    }

}
