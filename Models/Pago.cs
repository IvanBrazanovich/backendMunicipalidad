using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMunicipalidadTP.Models.MunicipalidadTPApi.Models;

namespace WebMunicipalidadTP.Models
{

    [Table("Pago")] // Especificar el nombre de la tabla correcta
    public class Pago
    {
        [Key] public int Id_Pago { get; set; } // Clave primaria
        public int? Id_cuota { get; set; }
        public DateTime? Fecha_pago { get; set; }
        public decimal? Monto_pagado { get; set; }

        public Cuota? Cuota { get; set; }
        public ICollection<PagoPropietario>? PagosPropietarios { get; set; }
    }
}
