using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using EduWork.BusinessLayer.Services;
using Common.DTO;

namespace EduWork.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly UserProfileService _userProfileService;

        public UserProfilesController(UserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAllUserProfiles()
        {
            return await _userProfileService.GetAllUserProfiles();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile(int id)
        {
            var user = await _userProfileService.GetUserProfile(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("sickLeave/{id}")]
        public async Task<ActionResult<IEnumerable<SickLeaveRecordDto>>> GetUserSickLeaveRecords(int id)
        {
            var userSickLeaveRecords = await _userProfileService.GetUserSickLeaveRecords(id);

            if (!userSickLeaveRecords.Any())
            {
                return NotFound();
            }

            return userSickLeaveRecords;
        }

        [HttpGet("annualLeave/{id}")]
        public async Task<ActionResult<IEnumerable<AnnualLeaveDto>>> GetUserAnnualLeave(int id)
        {
            var userAnnualLeave = await _userProfileService.GetUserAnnualLeave(id);

            if (!userAnnualLeave.Any())
            {
                return NotFound();
            }

            return userAnnualLeave;
        }

        //// PUT: api/UserProfiles/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _userProfileService.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/UserProfiles
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        //// DELETE: api/UserProfiles/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
