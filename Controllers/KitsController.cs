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
    public class KitsController : ControllerBase
    {
        private readonly MiEstiloContext _context;

        public KitsController(MiEstiloContext context)
        {
            _context = context;
        }

        // GET: api/Kits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kit>>> GetKits()
        {
            return await _context.Kits.ToListAsync();
        }

        // GET: api/Kits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kit>> GetKit(int id)
        {
            var kit = await _context.Kits.FindAsync(id);

            if (kit == null)
            {
                return NotFound();
            }

            return kit;
        }

        // PUT: api/Kits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKit(int id, Kit kit)
        {
            if (id != kit.IdKit)
            {
                return BadRequest();
            }

            _context.Entry(kit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KitExists(id))
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

        // POST: api/Kits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kit>> PostKit(Kit kit)
        {
            _context.Kits.Add(kit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKit", new { id = kit.IdKit }, kit);
        }

        // DELETE: api/Kits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKit(int id)
        {
            var kit = await _context.Kits.FindAsync(id);
            if (kit == null)
            {
                return NotFound();
            }

            _context.Kits.Remove(kit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KitExists(int id)
        {
            return _context.Kits.Any(e => e.IdKit == id);
        }
    }
}
