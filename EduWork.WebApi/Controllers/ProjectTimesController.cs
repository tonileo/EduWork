using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EduWork.BusinessLayer.Contracts;
using EduWork.WebApi.Authentication;
using Common.DTO.ProjectTime;

namespace EduWork.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTimesController(IUserProjectTimeService _userProjectTimeService) : ControllerBase
    {
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<ProjectSmallDto>>> GetProjects([FromServices] IIdentityClaim currentUser)
        {
            var result = await _userProjectTimeService.GetProjects(currentUser.Email);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("usernames")]
        public async Task<ActionResult<IEnumerable<UsernamesDto>>> GetUsernames()
        {
            var result = await _userProjectTimeService.GetUsernames();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<InputProjectTimeResponseDto>> GetMyInputProjectTimes([FromServices] IIdentityClaim currentUser, [FromQuery] DateTime userWorkDay)
        {
            var result = await _userProjectTimeService.GetMyInputProjectTimes(currentUser.Email, userWorkDay);
            return Ok(result);
        }

        [HttpGet("overtime")]
        public async Task<ActionResult<OvertimeDto>> GetOverTime([FromServices] IIdentityClaim currentUser)
        {
            var result = await _userProjectTimeService.GetOverTime(currentUser.Email);
            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<ActionResult<InputProjectTimeDto>> GetMyProjectTimes([FromServices] IIdentityClaim currentUser, [FromQuery] DateTime userWorkDay)
        {
            var result = await _userProjectTimeService.GetMyProjectTimes(currentUser.Email, userWorkDay);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("history/admin")]
        public async Task<ActionResult<IEnumerable<InputProjectTimeDto>>> GetProjectTimes(string username, [FromQuery] DateTime userWorkDay)
        {
            var result = await _userProjectTimeService.GetProjectTimes(username, userWorkDay);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> InputProjectTime([FromServices] IIdentityClaim currentUser, ProjectTimeRequestDto projectTime)
        {
            await _userProjectTimeService.InputProjectTime(currentUser.Email!, projectTime);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProjectTime([FromServices] IIdentityClaim currentUser, int id, ProjectTimeRequestDto projectTime)
        {
            await _userProjectTimeService.UpdateProjectTime(currentUser.Email!, id, projectTime);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectTime(int id)
        {
            await _userProjectTimeService.DeleteProjectTime(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("statsFilter/admin")]
        public async Task<ActionResult<ProjectTimeResponseDto>> GetProjectTimesFilter([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? username, [FromQuery] string? projectTitle)
        {
            var result = await _userProjectTimeService.GetProjectTimesFilter(fromDate, toDate, username, projectTitle);
            return Ok(result);
        }

        [HttpGet("statsFilter")]
        public async Task<ActionResult<ProjectTimeResponseDto>> GetMyProjectTimesFilter([FromServices] IIdentityClaim currentUser, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? projectTitle)
        {
            var result = await _userProjectTimeService.GetMyProjectTimesFilter(currentUser.Email, fromDate, toDate, projectTitle);
            return Ok(result);
        }

        [HttpGet("historyFilter")]
        public async Task<ActionResult<ProjectTimeHistoryDto>> GetMyHistoryProjectTimesFilter([FromServices] IIdentityClaim currentUser, [FromQuery] bool? thisMonth, [FromQuery] bool? lastMonth, [FromQuery] bool? thisQuarter)
        {
            var result = await _userProjectTimeService.GetMyHistoryProjectTimesFilter(currentUser.Email, thisMonth, lastMonth, thisQuarter);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("historyFilter/admin")]
        public async Task<ActionResult<ProjectTimeHistoryDto>> GetHistoryProjectTimesFilter([FromQuery] bool? thisMonth, [FromQuery] bool? lastMonth, [FromQuery] bool? thisQuarter, [FromQuery] string? username)
        {
            var result = await _userProjectTimeService.GetHistoryProjectTimesFilter(thisMonth, lastMonth, thisQuarter, username);
            return Ok(result);
        }
    }
}
