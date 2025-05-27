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
    public class ListasDeseosController : ControllerBase
    {
        private readonly MiEstiloContext _context;

        public ListasDeseosController(MiEstiloContext context)
        {
            _context = context;
        }

        // GET: api/ListasDeseos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListasDeseo>>> GetListasDeseos()
        {
            return await _context.ListasDeseos.ToListAsync();
        }

        // GET: api/ListasDeseos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListasDeseo>> GetListasDeseo(int id)
        {
            var listasDeseo = await _context.ListasDeseos.FindAsync(id);

            if (listasDeseo == null)
            {
                return NotFound();
            }

            return listasDeseo;
        }

        // PUT: api/ListasDeseos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListasDeseo(int id, ListasDeseo listasDeseo)
        {
            if (id != listasDeseo.IdListaDeseo)
            {
                return BadRequest();
            }

            _context.Entry(listasDeseo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListasDeseoExists(id))
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

        // POST: api/ListasDeseos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListasDeseo>> PostListasDeseo(ListasDeseo ProductoDeseo)
        {
            ProductoDeseo.FechaAgregado = DateTime.Now;

            _context.ListasDeseos.Add(ProductoDeseo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListasDeseo", new { id = ProductoDeseo.IdListaDeseo }, ProductoDeseo);
        }

        // DELETE: api/ListasDeseos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListasDeseo(int id)
        {
            var listasDeseo = await _context.ListasDeseos.FindAsync(id);
            if (listasDeseo == null)
            {
                return NotFound();
            }

            _context.ListasDeseos.Remove(listasDeseo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListasDeseoExists(int id)
        {
            return _context.ListasDeseos.Any(e => e.IdListaDeseo == id);
        }
    }
}
