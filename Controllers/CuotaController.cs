using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalidadTPApi.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMunicipalidadTP.Models.MunicipalidadTPApi.Models;

namespace MunicipalidadTPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuotasController : ControllerBase
    {
        private readonly MunicipalidadContext _context;

        public CuotasController(MunicipalidadContext context)
        {
            _context = context;
        }

        // GET: api/Cuotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuota>>> GetCuotas()
        {
            return await _context.Cuotas
                .Include(c => c.Bien) // Incluir la relación con Bien si es necesario
                .Include(c => c.Estado) // Incluir la relación con Estado si es necesario
                .ToListAsync();
        }

        // GET: api/Cuotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuota>> GetCuota(int id)
        {
            var cuota = await _context.Cuotas
                .Include(c => c.Bien) // Incluir la relación con Bien si es necesario
                .Include(c => c.Estado) // Incluir la relación con Estado si es necesario
                .FirstOrDefaultAsync(c => c.Idcuota == id);

            if (cuota == null)
            {
                return NotFound();
            }

            return cuota;
        }

        // POST: api/Cuotas
        [HttpPost]
        public async Task<ActionResult<Cuota>> PostCuota(Cuota cuota)
        {
            _context.Cuotas.Add(cuota);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuota), new { id = cuota.Idcuota }, cuota);
        }

        // PUT: api/Cuotas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuota(int id, Cuota cuota)
        {
            if (id != cuota.Idcuota)
            {
                return BadRequest();
            }

            _context.Entry(cuota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuotaExists(id))
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

        // DELETE: api/Cuotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuota(int id)
        {
            var cuota = await _context.Cuotas.FindAsync(id);
            if (cuota == null)
            {
                return NotFound();
            }

            _context.Cuotas.Remove(cuota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuotaExists(int id)
        {
            return _context.Cuotas.Any(e => e.Idcuota == id);
        }
    }
}
