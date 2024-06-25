using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalidadTPApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMunicipalidadTP.Models;
using WebMunicipalidadTP.Models.MunicipalidadTPApi.Models; // Añadir el espacio de nombres correcto
namespace MunicipalidadTPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BienController : ControllerBase
    {
        private readonly MunicipalidadContext _context;

        public BienController(MunicipalidadContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bien>>> GetBienes()
        {
            return await _context.Bienes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bien>> GetBien(int id)
        {
            var bien = await _context.Bienes.FindAsync(id);

            if (bien == null)
            {
                return NotFound();
            }

            return bien;
        }

        [HttpPost]
        public async Task<ActionResult<Bien>> PostBien(BienPayload bienPayload)
        {

            var bien = new Bien
            {
                Tipo = bienPayload.Tipo,
                Direccion = bienPayload.Direccion,
                Superficie = bienPayload.Superficie,
                Nomenclatura_catastral = bienPayload.Nomenclatura_catastral,
                Marca = bienPayload.Marca,
                Modelo = bienPayload.Modelo,
                Anio = bienPayload.Anio,
                Patente = bienPayload.Patente,
                TipoBien = bienPayload.TipoBien,
                Cuotas = bienPayload.Cuotas,
                PagosPropietarios = bienPayload.PagosPropietarios
            };


            if (bienPayload.Propietarios != null)
            {
                foreach (var propietario in bienPayload.Propietarios)
                {
                    var propietarioExistente = await _context.Propietarios.FindAsync(propietario.IDPropietario);
                    if (propietarioExistente != null)
                    {
                        _context.Entry(propietarioExistente).CurrentValues.SetValues(propietario);
                    }
                    else
                    {
                        _context.Propietarios.Add(propietario);
                    }
                }
            }

            _context.Bienes.Add(bien);



            if (bienPayload.Propietarios != null)
            {
                foreach (var propietario in bienPayload.Propietarios)
                {
                    var propietarioExistente = await _context.Propietarios.FindAsync(propietario.IDPropietario);
                    if (propietarioExistente != null)
                    {
                        _context.Entry(propietarioExistente).CurrentValues.SetValues(propietario);
                    }
                    else
                    {
                        _context.Propietarios.Add(propietario);
                    }

                    var bienPropietario = new BienPropietario
                    {
                        Bien = bien,
                        Propietario = propietarioExistente ?? propietario,
                        Porcentaje_Propiedad = 100// Asegúrate de definir esta propiedad en Propietario
                    };

                    _context.BienesPropietarios.Add(bienPropietario);
                }
            }
            if (bienPayload.Cuotas != null)
            {
                foreach (var cuota in bienPayload.Cuotas)
                {
                    _context.Entry(cuota).State = EntityState.Added;
                }
            }

            if (bienPayload.PagosPropietarios != null)
            {
                foreach (var pagoPropietario in bienPayload.PagosPropietarios)
                {
                    _context.Entry(pagoPropietario).State = EntityState.Added;
                }
            }












            await _context.SaveChangesAsync();

            // Crear automáticamente 6 cuotas cada 2 meses
            var fechaVencimiento = DateTime.Today;

            for (int i = 0; i < 6; i++)
            {
                fechaVencimiento = fechaVencimiento.AddMonths(2);

                var cuota = new Cuota
                {
                    Id_bien = bien.Idbien, // Asignar el Id del bien creado
                    Monto = 10000, // Asignar el monto correcto
                    Fecha_vencimiento = fechaVencimiento,
                    CodEstado = 3 // Asignar el estado correcto
                };

                // Agregar la cuota a la base de datos
                _context.Cuotas.Add(cuota);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetBien), new { id = bien.Idbien }, bien);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> PutBien(int id, Bien bien)
        {
            if (id != bien.Idbien)
            {
                return BadRequest();
            }

            _context.Entry(bien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBien(int id)
        {
            var bien = await _context.Bienes.FindAsync(id);
            if (bien == null)
            {
                return NotFound();
            }

            _context.Bienes.Remove(bien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("automoviles")]
        public async Task<ActionResult<IEnumerable<object>>> GetAutomoviles()
        {
            var automoviles = await _context.Bienes
                .Where(b => b.Tipo == 1) // Tipo 1 para automóviles
                .Select(b => new
                {
                    b.Idbien,
                    b.Marca,
                    b.Modelo,
                    b.Anio,
                    b.Patente,
                    Propietarios = b.BienesPropietarios.Select(bp => new
                    {
                        bp.Propietario.IDPropietario,
                        bp.Propietario.ApeyNombre,
                        bp.Propietario.Numdoc,
                        bp.Propietario.Direccion,
                        bp.Propietario.Email,
                        bp.Propietario.Fechanac,
                        EstadoCivil = bp.Propietario.EstadoCivil.Descripcion,
                        bp.Propietario.Tipo
                    }).ToList(),
                    Cuotas = _context.Cuotas
                        .Where(c => c.Id_bien == b.Idbien)
                        .Select(c => new
                        {
                            c.Idcuota,
                            c.Monto,
                            c.Fecha_vencimiento,
                            Estado = c.Estado.Descripcion
                        })
                        .ToList(),
                    PagosHechos = _context.PagosPropietarios
                .Where(pp => pp.Id_Bien == b.Idbien)
                .Select(pp => new
                {
                    pp.Pago.Id_Pago,
                    pp.Pago.Fecha_pago,
                    pp.Pago.Monto_pagado
                })
                .Distinct() // Evitar duplicados
                .ToList()
                })
                .ToListAsync();

            return automoviles;
        }

        [HttpGet("inmuebles")]
        public async Task<ActionResult<IEnumerable<object>>> GetInmuebles()
        {
            var inmuebles = await _context.Bienes
                .Where(b => b.Tipo == 2) // Tipo 2 para bienes inmuebles
                .Select(b => new
                {
                    b.Idbien,
                    Direccion = b.Direccion,
                    Superficie = b.Superficie,
                    b.Anio,
                    Nomenclatura_Catastral = b.Nomenclatura_catastral,
                    Propietarios = b.BienesPropietarios.Select(bp => new
                    {
                        bp.Propietario.IDPropietario,
                        bp.Propietario.ApeyNombre,
                        bp.Propietario.Numdoc,
                        bp.Propietario.Direccion,
                        bp.Propietario.Email,
                        bp.Propietario.Fechanac,
                        EstadoCivil = bp.Propietario.EstadoCivil.Descripcion,
                        bp.Propietario.Tipo
                    }).ToList(),
                    Cuotas = _context.Cuotas
                        .Where(c => c.Id_bien == b.Idbien)
                        .Select(c => new
                        {
                            c.Idcuota,
                            c.Monto,
                            c.Fecha_vencimiento,
                            Estado = c.Estado.Descripcion
                        })
                        .ToList(),
                    PagosHechos = _context.PagosPropietarios
                    .Where(pp => pp.Id_Bien == b.Idbien)
                    .Select(pp => new
                    {
                        pp.Pago.Id_Pago,
                        pp.Pago.Fecha_pago,
                        pp.Pago.Monto_pagado
                    })
                    .Distinct() // Evitar duplicados
                    .ToList()
                })
                .ToListAsync();

            return inmuebles;
        }


        private bool BienExists(int id)
        {
            return _context.Bienes.Any(e => e.Idbien == id);
        }
    }
}


public class BienPayload
{
    public int? Tipo { get; set; }
    public string? Direccion { get; set; }
    public decimal? Superficie { get; set; }
    public string? Nomenclatura_catastral { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int? Anio { get; set; }
    public string? Patente { get; set; }

    public CDtipoBien? TipoBien { get; set; }
    public ICollection<Cuota>? Cuotas { get; set; }
    public ICollection<PagoPropietario>? PagosPropietarios { get; set; }
    public ICollection<Propietario>? Propietarios { get; set; }
}