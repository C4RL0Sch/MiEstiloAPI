using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiEstiloAPI.Models;

namespace MiEstiloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaracteristicasController : ControllerBase
    {
        private readonly MiEstiloContext _context;

        public CaracteristicasController(MiEstiloContext context)
        {
            _context = context;
        }

        // GET: api/Caracteristicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caracteristica>>> GetCaracteristicas()
        {
            return await _context.Caracteristicas.ToListAsync();
        }

        // GET: api/Caracteristicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Caracteristica>> GetCaracteristica(int id)
        {
            var caracteristica = await _context.Caracteristicas.FindAsync(id);

            if (caracteristica == null)
            {
                return NotFound();
            }

            return caracteristica;
        }

        // PUT: api/Caracteristicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaracteristica(int id, Caracteristica caracteristica)
        {
            if (id != caracteristica.IdCaracteristica)
            {
                return BadRequest();
            }

            _context.Entry(caracteristica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaracteristicaExists(id))
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

        // POST: api/Caracteristicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Caracteristica>> PostCaracteristica(Caracteristica caracteristica)
        {
            _context.Caracteristicas.Add(caracteristica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaracteristica", new { id = caracteristica.IdCaracteristica }, caracteristica);
        }

        // DELETE: api/Caracteristicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaracteristica(int id)
        {
            var caracteristica = await _context.Caracteristicas.FindAsync(id);
            if (caracteristica == null)
            {
                return NotFound();
            }

            _context.Caracteristicas.Remove(caracteristica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaracteristicaExists(int id)
        {
            return _context.Caracteristicas.Any(e => e.IdCaracteristica == id);
        }
    }
}
