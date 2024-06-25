using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalidadTPApi.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMunicipalidadTP.Models;

namespace MunicipalidadTPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly MunicipalidadContext _context;

        public PagoController(MunicipalidadContext context)
        {
            _context = context;
        }

        // GET: api/Pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pagos.ToListAsync();
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // POST: api/Pago
        // POST: api/Pago
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(PagoPayload objeto)
        {
            // Crear el objeto Pago a partir del payload
            var pago = new Pago
            {
                Id_cuota = objeto.Idcuota,
                Fecha_pago = objeto.Fecha_pago,
                Monto_pagado = objeto.Monto_pagado
            };

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            // Actualizar la tabla PagoPropietario si existen propietarios asociados al pago
            if (objeto.propietarios != null && objeto.propietarios.Any())
            {
                foreach (var propietarioPayload in objeto.propietarios)
                {
                    // Buscar el propietario en la base de datos por su Id
                    var propietario = await _context.Propietarios
                        .FirstOrDefaultAsync(p => p.IDPropietario == propietarioPayload.idPropietario);

                    if (propietario == null)
                    {
                        // Si el propietario no existe, puedes manejarlo según tus requisitos,
                        // por ejemplo, lanzar una excepción o registrar un error.
                        return BadRequest($"El propietario con ID {propietarioPayload.idPropietario} no existe.");
                    }

                    // Buscar el bien en la base de datos por su Id
                    var bien = await _context.Bienes
                        .FirstOrDefaultAsync(b => b.Idbien == objeto.Idbien);

                    if (bien == null)
                    {
                        // Si el bien no existe, puedes manejarlo según tus requisitos,
                        // por ejemplo, lanzar una excepción o registrar un error.
                        return BadRequest($"El bien con ID {objeto.Idbien} no existe.");
                    }

                    // Crear el objeto PagoPropietario y asignar el propietario y el bien recuperados
                    var pagoPropietario = new PagoPropietario
                    {
                        Id_Pago = pago.Id_Pago,
                        Id_Propietario = propietario.IDPropietario,
                        Id_Bien = bien.Idbien,
                        Propietario = propietario,
                        Bien = bien // Asignar el bien recuperado
                    };

                    _context.PagosPropietarios.Add(pagoPropietario);
                }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPago), new { id = pago.Id_Pago }, pago);
        }


        // PUT: api/Pago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.Id_Pago)
            {
                return BadRequest();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.Id_Pago == id);
        }
    }
}


public class PagoPayload
{
    public int Idcuota { get; set; }
    public int Idbien { get; set; }
    public DateTime Fecha_pago { get; set; }
    public decimal Monto_pagado { get; set; }
    public List<PropietarioPayload> propietarios { get; set; }
}

public class PropietarioPayload
{
    public string apeyNombre { get; set; }
    public string? direccion { get; set; }
    public string email { get; set; }
    public object? estadoCivil { get; set; }
    public object? fechanac { get; set; }
    public int idPropietario { get; set; }
    public object? numdoc { get; set; }
    public int tipo { get; set; }
}
