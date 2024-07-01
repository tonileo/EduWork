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
    public class NonWorkingDaysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NonWorkingDaysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/NonWorkingDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NonWorkingDay>>> GetNonWorkingDays()
        {
            return await _context.NonWorkingDays.ToListAsync();
        }

        // GET: api/NonWorkingDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NonWorkingDay>> GetNonWorkingDay(int id)
        {
            var nonWorkingDay = await _context.NonWorkingDays.FindAsync(id);

            if (nonWorkingDay == null)
            {
                return NotFound();
            }

            return nonWorkingDay;
        }

        // PUT: api/NonWorkingDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNonWorkingDay(int id, NonWorkingDay nonWorkingDay)
        {
            if (id != nonWorkingDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(nonWorkingDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NonWorkingDayExists(id))
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

        // POST: api/NonWorkingDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NonWorkingDay>> PostNonWorkingDay(NonWorkingDay nonWorkingDay)
        {
            _context.NonWorkingDays.Add(nonWorkingDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNonWorkingDay", new { id = nonWorkingDay.Id }, nonWorkingDay);
        }

        // DELETE: api/NonWorkingDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNonWorkingDay(int id)
        {
            var nonWorkingDay = await _context.NonWorkingDays.FindAsync(id);
            if (nonWorkingDay == null)
            {
                return NotFound();
            }

            _context.NonWorkingDays.Remove(nonWorkingDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NonWorkingDayExists(int id)
        {
            return _context.NonWorkingDays.Any(e => e.Id == id);
        }
    }
}
