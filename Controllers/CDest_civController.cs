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
    public class CDest_civController : ControllerBase
    {
        private readonly MunicipalidadContext _context;

        public CDest_civController(MunicipalidadContext context)
        {
            _context = context;
        }

        // GET: api/CDest_civ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CDest_civ>>> GetCDest_civ()
        {
            return await _context.EstadosCiviles.ToListAsync();
        }

        // GET: api/CDest_civ/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CDest_civ>> GetCDest_civ(int id)
        {
            var cDest_civ = await _context.EstadosCiviles.FindAsync(id);

            if (cDest_civ == null)
            {
                return NotFound();
            }

            return cDest_civ;
        }

        // POST: api/CDest_civ
        [HttpPost]
        public async Task<ActionResult<CDest_civ>> PostCDest_civ(CDest_civ cDest_civ)
        {
            _context.EstadosCiviles.Add(cDest_civ);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCDest_civ), new { id = cDest_civ.Codigo }, cDest_civ);
        }

        // PUT: api/CDest_civ/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCDest_civ(int id, CDest_civ cDest_civ)
        {
            if (id != cDest_civ.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(cDest_civ).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CDest_civExists(id))
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

        // DELETE: api/CDest_civ/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCDest_civ(int id)
        {
            var cDest_civ = await _context.EstadosCiviles.FindAsync(id);
            if (cDest_civ == null)
            {
                return NotFound();
            }

            _context.EstadosCiviles.Remove(cDest_civ);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CDest_civExists(int id)
        {
            return _context.EstadosCiviles.Any(e => e.Codigo == id);
        }
    }
}
