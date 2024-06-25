using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalidadTPApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMunicipalidadTP.Models;

namespace MunicipalidadTPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropietariosController : ControllerBase
    {
        private readonly MunicipalidadContext _context;

        public PropietariosController(MunicipalidadContext context)
        {
            _context = context;
        }

        // GET: api/Propietarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Propietario>>> GetPropietarios()
        {
            return await _context.Propietarios.ToListAsync();
        }

        // GET: api/Propietarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Propietario>> GetPropietario(int id)
        {
            var propietario = await _context.Propietarios.FindAsync(id);

            if (propietario == null)
            {
                return NotFound();
            }

            return propietario;
        }

        // POST: api/Propietarios
        [HttpPost]
        public async Task<ActionResult<Propietario>> PostPropietario(Propietario propietario)
        {
            _context.Propietarios.Add(propietario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPropietario), new { id = propietario.IDPropietario }, propietario);
        }

        // PUT: api/Propietarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropietario(int id, Propietario propietario)
        {
            if (id != propietario.IDPropietario)
            {
                return BadRequest();
            }

            _context.Entry(propietario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropietarioExists(id))
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

        // DELETE: api/Propietarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropietario(int id)
        {
            var propietario = await _context.Propietarios.FindAsync(id);
            if (propietario == null)
            {
                return NotFound();
            }

            _context.Propietarios.Remove(propietario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Propietarios/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Propietario>> Login(LoginRequest propietarioLogin)
        {
            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p => 
                p.Email == propietarioLogin.Email && p.Password == propietarioLogin.Password);

            if (propietario == null)
            {
                return NotFound("Usuario o contraseña incorrectos");
            }

            return propietario;
        }



        private bool PropietarioExists(int id)
        {
            return _context.Propietarios.Any(e => e.IDPropietario == id);
        }
    }
}


public class LoginRequest {
    public string Email { get; set; }
    public string Password { get; set; }
}