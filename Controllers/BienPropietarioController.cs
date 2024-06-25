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
    public class BienPropietarioController : ControllerBase
    {
        private readonly MunicipalidadContext _context;

        public BienPropietarioController(MunicipalidadContext context)
        {
            _context = context;
        }

        // GET: api/BienPropietario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BienPropietario>>> GetBienPropietarios()
        {
            return await _context.BienesPropietarios.ToListAsync();
        }

        // GET: api/BienPropietario/5/Propietario
        [HttpGet("{idPropietario}/Propietario")]
        public async Task<ActionResult<IEnumerable<BienPropietario>>> GetBienPropietariosByPropietario(int idPropietario)
        {
            var bienPropietarios = await _context.BienesPropietarios
                .Where(bp => bp.Id_Propietario == idPropietario)
                .ToListAsync();

            if (bienPropietarios == null || bienPropietarios.Count == 0)
            {
                return NotFound();
            }

            return bienPropietarios;
        }

        // GET: api/BienPropietario/5/Bien
        [HttpGet("{idBien}/Bien")]
        public async Task<ActionResult<IEnumerable<BienPropietario>>> GetBienPropietariosByBien(int idBien)
        {
            var bienPropietarios = await _context.BienesPropietarios
                .Where(bp => bp.Id_Bien == idBien)
                .ToListAsync();

            if (bienPropietarios == null || bienPropietarios.Count == 0)
            {
                return NotFound();
            }

            return bienPropietarios;
        }

        // GET: api/BienPropietario/5/Propietario/10/Bien
        [HttpGet("{idPropietario}/Propietario/{idBien}/Bien")]
        public async Task<ActionResult<BienPropietario>> GetBienPropietario(int idPropietario, int idBien)
        {
            var bienPropietario = await _context.BienesPropietarios
                .FirstOrDefaultAsync(bp => bp.Id_Propietario == idPropietario && bp.Id_Bien == idBien);

            if (bienPropietario == null)
            {
                return NotFound();
            }

            return bienPropietario;
        }

        // POST: api/BienPropietario
        [HttpPost]
        public async Task<ActionResult<BienPropietario>> PostBienPropietario(BienPropietario bienPropietario)
        {
            _context.BienesPropietarios.Add(bienPropietario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBienPropietario), new { idPropietario = bienPropietario.Id_Propietario, idBien = bienPropietario.Id_Bien }, bienPropietario);
        }

        // PUT: api/BienPropietario/5/Propietario/10/Bien
        [HttpPut("{idPropietario}/Propietario/{idBien}/Bien")]
        public async Task<IActionResult> PutBienPropietario(int idPropietario, int idBien, BienPropietario bienPropietario)
        {
            if (idPropietario != bienPropietario.Id_Propietario || idBien != bienPropietario.Id_Bien)
            {
                return BadRequest();
            }

            _context.Entry(bienPropietario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BienPropietarioExists(idPropietario, idBien))
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

        // DELETE: api/BienPropietario/5/Propietario/10/Bien
        [HttpDelete("{idPropietario}/Propietario/{idBien}/Bien")]
        public async Task<IActionResult> DeleteBienPropietario(int idPropietario, int idBien)
        {
            var bienPropietario = await _context.BienesPropietarios
                .FirstOrDefaultAsync(bp => bp.Id_Propietario == idPropietario && bp.Id_Bien == idBien);

            if (bienPropietario == null)
            {
                return NotFound();
            }

            _context.BienesPropietarios.Remove(bienPropietario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BienPropietarioExists(int idPropietario, int idBien)
        {
            return _context.BienesPropietarios.Any(bp => bp.Id_Propietario == idPropietario && bp.Id_Bien == idBien);
        }
    }
}
