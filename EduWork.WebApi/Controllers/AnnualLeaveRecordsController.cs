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
    public class AnnualLeaveRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnnualLeaveRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AnnualLeaveRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnualLeaveRecord>>> GetAnnualLeavesRecords()
        {
            return await _context.AnnualLeavesRecords.ToListAsync();
        }

        // GET: api/AnnualLeaveRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnualLeaveRecord>> GetAnnualLeaveRecord(int id)
        {
            var annualLeaveRecord = await _context.AnnualLeavesRecords.FindAsync(id);

            if (annualLeaveRecord == null)
            {
                return NotFound();
            }

            return annualLeaveRecord;
        }

        // PUT: api/AnnualLeaveRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnualLeaveRecord(int id, AnnualLeaveRecord annualLeaveRecord)
        {
            if (id != annualLeaveRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(annualLeaveRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnualLeaveRecordExists(id))
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

        // POST: api/AnnualLeaveRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnnualLeaveRecord>> PostAnnualLeaveRecord(AnnualLeaveRecord annualLeaveRecord)
        {
            _context.AnnualLeavesRecords.Add(annualLeaveRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnualLeaveRecord", new { id = annualLeaveRecord.Id }, annualLeaveRecord);
        }

        // DELETE: api/AnnualLeaveRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnualLeaveRecord(int id)
        {
            var annualLeaveRecord = await _context.AnnualLeavesRecords.FindAsync(id);
            if (annualLeaveRecord == null)
            {
                return NotFound();
            }

            _context.AnnualLeavesRecords.Remove(annualLeaveRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnualLeaveRecordExists(int id)
        {
            return _context.AnnualLeavesRecords.Any(e => e.Id == id);
        }
    }
}
