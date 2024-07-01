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
    public class WorkDaysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkDaysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDay>>> GetWorkDays()
        {
            return await _context.WorkDays.ToListAsync();
        }

        // GET: api/WorkDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkDay>> GetWorkDay(int id)
        {
            var workDay = await _context.WorkDays.FindAsync(id);

            if (workDay == null)
            {
                return NotFound();
            }

            return workDay;
        }

        // PUT: api/WorkDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkDay(int id, WorkDay workDay)
        {
            if (id != workDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(workDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkDayExists(id))
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

        // POST: api/WorkDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkDay>> PostWorkDay(WorkDay workDay)
        {
            _context.WorkDays.Add(workDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkDay", new { id = workDay.Id }, workDay);
        }

        // DELETE: api/WorkDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkDay(int id)
        {
            var workDay = await _context.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return NotFound();
            }

            _context.WorkDays.Remove(workDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkDayExists(int id)
        {
            return _context.WorkDays.Any(e => e.Id == id);
        }
    }
}
