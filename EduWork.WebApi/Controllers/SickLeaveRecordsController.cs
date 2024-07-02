using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace EduWork.WebApi.Controllers
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Route("api/[controller]")]
    [ApiController]
    public class SickLeaveRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SickLeaveRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SickLeaveRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SickLeaveRecord>>> GetSickLeaveRecords()
        {
            return await _context.SickLeaveRecords.ToListAsync();
        }

        // GET: api/SickLeaveRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SickLeaveRecord>> GetSickLeaveRecord(int id)
        {
            var sickLeaveRecord = await _context.SickLeaveRecords.FindAsync(id);

            if (sickLeaveRecord == null)
            {
                return NotFound();
            }

            return sickLeaveRecord;
        }

        // PUT: api/SickLeaveRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSickLeaveRecord(int id, SickLeaveRecord sickLeaveRecord)
        {
            if (id != sickLeaveRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(sickLeaveRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SickLeaveRecordExists(id))
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

        // POST: api/SickLeaveRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SickLeaveRecord>> PostSickLeaveRecord(SickLeaveRecord sickLeaveRecord)
        {
            _context.SickLeaveRecords.Add(sickLeaveRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSickLeaveRecord", new { id = sickLeaveRecord.Id }, sickLeaveRecord);
        }

        // DELETE: api/SickLeaveRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSickLeaveRecord(int id)
        {
            var sickLeaveRecord = await _context.SickLeaveRecords.FindAsync(id);
            if (sickLeaveRecord == null)
            {
                return NotFound();
            }

            _context.SickLeaveRecords.Remove(sickLeaveRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SickLeaveRecordExists(int id)
        {
            return _context.SickLeaveRecords.Any(e => e.Id == id);
        }
    }
}
