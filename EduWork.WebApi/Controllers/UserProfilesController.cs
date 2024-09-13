using Microsoft.AspNetCore.Mvc;
using Common.DTO.Profile;
using EduWork.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace EduWork.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController(IUserProfileService _userProfileService) : ControllerBase
    {
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<ProjectsProfileDto>>> GetProjects()
        {
            var result = await _userProfileService.GetProjects();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetUserSmallProfiles([FromQuery] string? username, [FromQuery] bool? asc)
        {
            return await _userProfileService.GetUserSmallProfiles(username, asc);
        }

        [HttpGet("profile/{username}")]
        public async Task<ActionResult<MyProfileDto>> GetUserProfile(string? username)
        {
            var result = await _userProfileService.GetUserProfile(username);
            return Ok(result);
        }

        [HttpGet("stats/{username}")]
        public async Task<ActionResult<MyProfileStatsDto>> GetMyProfileStats(string username, bool thisMonth, bool lastMonth)
        {
            var result = await _userProfileService.GetMyProfileStats(username, thisMonth, lastMonth);
            return Ok(result);
        }

        [HttpPost("addAnnualLeave/{username}")]
        public async Task<ActionResult> AddAnnualLeave(string username, ProfileAnnualRequestDto annualLeave)
        {
            await _userProfileService.AddAnnualLeave(username, annualLeave);
            return Ok();
        }

        [HttpPost("addSickDay/{username}")]
        public async Task<ActionResult> AddSickDay(string username, ProfileAnnualRequestDto annualLeave)
        {
            await _userProfileService.AddSickDay(username, annualLeave);
            return Ok();
        }
    }
}
