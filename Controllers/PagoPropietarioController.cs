using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMunicipalidadTP.Models;
using MunicipalidadTPApi.Data;

namespace MunicipalidadTPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoPropietarioController : ControllerBase
    {
        private readonly MunicipalidadContext _context;

        public PagoPropietarioController(MunicipalidadContext context)
        {
            _context = context;
        }

        // GET: api/PagoPropietario
        [HttpGet]
        public IActionResult GetPagosPropietarios()
        {
            var pagosPropietarios = _context.PagosPropietarios.ToList();
            return Ok(pagosPropietarios);
        }

        // GET: api/PagoPropietario/5
        [HttpGet("{id_pago}/{id_propietario}/{id_bien}")]
        public IActionResult GetPagoPropietario(int id_pago, int id_propietario, int id_bien)
        {
            var pagoPropietario = _context.PagosPropietarios.Find(id_pago, id_propietario, id_bien);

            if (pagoPropietario == null)
            {
                return NotFound();
            }

            return Ok(pagoPropietario);
        }

        // POST: api/PagoPropietario
        [HttpPost]
        public IActionResult PostPagoPropietario(PagoPropietario pagoPropietario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PagosPropietarios.Add(pagoPropietario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPagoPropietario), new { id = pagoPropietario.Id_Pago }, pagoPropietario);
        }

        // PUT: api/PagoPropietario/5
        [HttpPut("{id}")]
        public IActionResult PutPagoPropietario(int id, PagoPropietario pagoPropietario)
        {
            if (id != pagoPropietario.Id_Pago)
            {
                return BadRequest();
            }

            _context.Entry(pagoPropietario).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoPropietarioExists(id))
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

        // DELETE: api/PagoPropietario/5
        [HttpDelete("{id}")]
        public IActionResult DeletePagoPropietario(int id)
        {
            var pagoPropietario = _context.PagosPropietarios.Find(id);
            if (pagoPropietario == null)
            {
                return NotFound();
            }

            _context.PagosPropietarios.Remove(pagoPropietario);
            _context.SaveChanges();

            return NoContent();
        }

        private bool PagoPropietarioExists(int id)
        {
            return _context.PagosPropietarios.Any(e => e.Id_Pago == id);
        }
    }
}
