using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVManagementSystem.Data;
using CVManagementSystem.Models;
using Microsoft.Net.Http.Headers;
using System.Globalization;

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
        public async Task<ActionResult<IEnumerable<CV>>> GetCvs(int minimumGCSE = 0, string? professionalQualification = null, 
            string? educationQualification = null,int educationLevel = 0, int years = 0, string? skills = null, string? keyword = null)
        {
            if (_context.Cvs == null)
            {
                return NotFound();
            }
            var allCvs =  _context.Cvs;
            var inactive = _context.CVStatusTypes.Where(x => x.Name == "Inactive").FirstOrDefault();
            var query = allCvs.Where(x=>x.StatusID != inactive.ID).AsQueryable();

            if (allCvs != null)
            {
                if (minimumGCSE > 0)
                   query = query.Where(x => x.GCSEPasses >= minimumGCSE);
                if (years > 0)
                   query =  query.Where(x => x.ExperienceYears >= years);
                if (!String.IsNullOrEmpty(professionalQualification))
                {
                   query = query.Where(x => (x.ProfessionalQualification1 != null && x.ProfessionalQualification1.Contains(professionalQualification))
                        || (x.ProfessionalQualification2 != null && x.ProfessionalQualification2.Contains(professionalQualification))
                        || (x.ProfessionalQualification3 != null && x.ProfessionalQualification3.Contains(professionalQualification))
                        );
                }
                if (!String.IsNullOrEmpty(skills))
                    query = query.Where(x => x.Skills != null && x.Skills.Contains(skills));

                if (!String.IsNullOrEmpty(educationQualification))
                    query = query.Where(x => x.EducationQualification != null && x.EducationQualification.Contains(educationQualification));
                if (!String.IsNullOrEmpty(keyword))
                    query = query.Where(x => (x.FirstName + " " + x.LastName).Contains(keyword)
                    || (x.City != null && x.City.Contains(keyword)) || (x.AddressLine1 != null && x.AddressLine1.Contains(keyword))
                    || (x.AddressLine2 != null && x.AddressLine2.Contains(keyword))
                    || (x.Experience1 != null && x.Experience1.Contains(keyword))
                    || (x.Experience2 != null && x.Experience2.Contains(keyword)) || (x.Experience3 != null && x.Experience3.Contains(keyword))) ;
             if(educationLevel>0)
                {
                    switch(educationLevel)
                    {
                        case 1:
                            query = query.Where(x => string.IsNullOrEmpty(x.PrimaryEducation));
                            break;
                        case 2:
                            query = query.Where(x => string.IsNullOrEmpty(x.SecondaryEducation));
                            break;
                        case 3:
                            query = query.Where(x => string.IsNullOrEmpty(x.HigherEducation));
                            break;
                        default:
                            break;
                    }
                }
            }
            return await query.ToListAsync();
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
        public async Task<ActionResult<CV>> PostCV(CV cv = null)
        {
          if (_context.Cvs == null)
          {
              return Problem("Entity set 'CVContext.Cvs'  is null.");
          }
            cv.CreatedDate = DateTime.Now;
            cv.ModifiedDate= DateTime.Now;
            _context.Cvs.Add(cv);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCV", new { id = cv.ID }, cv);
        }

        // DELETE: api/CVs/5
        [HttpDelete("{id}")]
        public IResult DeleteCV(int id)
        {
            if (_context.Cvs == null)
            {
                return Results.NotFound();
            }
            var cV =  _context.Cvs.Find(id);
            if (cV == null)
            {
                return Results.NotFound();
            }
            var inactive = _context.CVStatusTypes.Where(x => x.Name == "Inactive").FirstOrDefault();
            cV.StatusID = inactive.ID;
            _context.Cvs.Remove(cV);
            _context.SaveChangesAsync();
            return Results.Ok();
        }

        private bool CVExists(int id)
        {
            var inactive = _context.CVStatusTypes.Where(x => x.Name == "Inactive").FirstOrDefault();
            return (_context.Cvs?.Any(e => e.ID == id && e.StatusID != inactive.ID )).GetValueOrDefault();
        }
    }
}
