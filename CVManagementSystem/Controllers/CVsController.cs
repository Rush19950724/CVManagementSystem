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
    public class CVsController : ControllerBase
    {
        private readonly CVContext _context;

        public CVsController(CVContext context)
        {
            _context = context;
        }

        // GET: api/CVs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CV>>> GetCvs()
        {
          if (_context.Cvs == null)
          {
              return NotFound();
          }
            return await _context.Cvs.ToListAsync();
        }

        // GET: api/CVs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CV>> GetCV(int id)
        {
          if (_context.Cvs == null)
          {
              return NotFound();
          }
            var cV = await _context.Cvs.FindAsync(id);

            if (cV == null)
            {
                return NotFound();
            }

            return cV;
        }

        // PUT: api/CVs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCV(int id, CV cV)
        {
            if (id != cV.ID)
            {
                return BadRequest();
            }

            cV.ModifiedDate = DateTime.Now;
            _context.Entry(cV).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CVExists(id))
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

        // POST: api/CVs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CV>> PostCV(CV cV)
        {
          if (_context.Cvs == null)
          {
              return Problem("Entity set 'CVContext.Cvs'  is null.");
          }
            cV.CreatedDate = DateTime.Now;
            _context.Cvs.Add(cV);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCV", new { id = cV.ID }, cV);
        }

        // DELETE: api/CVs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCV(int id)
        {
            if (_context.Cvs == null)
            {
                return NotFound();
            }
            var cV = await _context.Cvs.FindAsync(id);
            if (cV == null)
            {
                return NotFound();
            }

            _context.Cvs.Remove(cV);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CVExists(int id)
        {
            return (_context.Cvs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
