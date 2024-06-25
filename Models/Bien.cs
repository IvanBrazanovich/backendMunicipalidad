using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMunicipalidadTP.Models.MunicipalidadTPApi.Models;

namespace WebMunicipalidadTP.Models
{

    [Table("Bien")] // Especificar el nombre de la tabla correcta
    public class Bien
   
    {

        [Key] public int Idbien { get; set; } // Clave primaria
        public int? Tipo { get; set; }
        public string? Direccion { get; set; }
        public decimal? Superficie { get; set; }
        public string? Nomenclatura_catastral { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int? Anio { get; set; }
        public string? Patente { get; set; }

        public CDtipoBien? TipoBien { get; set; }
        public ICollection<BienPropietario>? BienesPropietarios { get; set; }
        public ICollection<Cuota>? Cuotas { get; set; }
        public ICollection<PagoPropietario>? PagosPropietarios { get; set; }

    }
}
