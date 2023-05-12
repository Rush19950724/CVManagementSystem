using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVManagementSystem.Data;
using CVManagementSystem.Models;

namespace CVManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVCollectionsController : ControllerBase
    {
        private readonly CVContext _context;

        public CVCollectionsController(CVContext context)
        {
            _context = context;
        }

        // GET: api/CVCollections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CVCollection>>> GetCVCollections()
        {
          if (_context.CVCollections == null)
          {
              return NotFound();
          }
            return await _context.CVCollections.ToListAsync();
        }

        // GET: api/CVCollections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CVCollection>> GetCVCollection(int id)
        {
          if (_context.CVCollections == null)
          {
              return NotFound();
          }
            var cVCollection = await _context.CVCollections.FindAsync(id);

            if (cVCollection == null)
            {
                return NotFound();
            }

            return cVCollection;
        }

        // PUT: api/CVCollections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCVCollection(int id, CVCollection cVCollection)
        {
            if (id != cVCollection.ID)
            {
                return BadRequest();
            }

            _context.Entry(cVCollection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CVCollectionExists(id))
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

        // POST: api/CVCollections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CVCollection>> PostCVCollection(CVCollection cVCollection)
        {
          if (_context.CVCollections == null)
          {
              return Problem("Entity set 'CVContext.CVCollections'  is null.");
          }
            _context.CVCollections.Add(cVCollection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCVCollection", new { id = cVCollection.ID }, cVCollection);
        }

        // DELETE: api/CVCollections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCVCollection(int id)
        {
            if (_context.CVCollections == null)
            {
                return NotFound();
            }
            var cVCollection = await _context.CVCollections.FindAsync(id);
            if (cVCollection == null)
            {
                return NotFound();
            }

            _context.CVCollections.Remove(cVCollection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CVCollectionExists(int id)
        {
            return (_context.CVCollections?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
