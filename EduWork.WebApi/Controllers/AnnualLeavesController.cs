using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;

namespace EduWork.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnualLeavesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnnualLeavesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AnnualLeaves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnualLeave>>> GetAnnualLeaves()
        {
            return await _context.AnnualLeaves.ToListAsync();
        }

        // GET: api/AnnualLeaves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnualLeave>> GetAnnualLeave(int id)
        {
            var annualLeave = await _context.AnnualLeaves.FindAsync(id);

            if (annualLeave == null)
            {
                return NotFound();
            }

            return annualLeave;
        }

        // PUT: api/AnnualLeaves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnualLeave(int id, AnnualLeave annualLeave)
        {
            if (id != annualLeave.Id)
            {
                return BadRequest();
            }

            _context.Entry(annualLeave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnualLeaveExists(id))
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

        // POST: api/AnnualLeaves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnnualLeave>> PostAnnualLeave(AnnualLeave annualLeave)
        {
            _context.AnnualLeaves.Add(annualLeave);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnualLeave", new { id = annualLeave.Id }, annualLeave);
        }

        // DELETE: api/AnnualLeaves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnualLeave(int id)
        {
            var annualLeave = await _context.AnnualLeaves.FindAsync(id);
            if (annualLeave == null)
            {
                return NotFound();
            }

            _context.AnnualLeaves.Remove(annualLeave);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnualLeaveExists(int id)
        {
            return _context.AnnualLeaves.Any(e => e.Id == id);
        }
    }
}
