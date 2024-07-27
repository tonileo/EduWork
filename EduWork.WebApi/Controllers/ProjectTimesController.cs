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
using EduWork.BusinessLayer.Services;
using EduWork.BusinessLayer.Contracts;
using EduWork.WebApi.Authentication;
using Common.DTO.ProjectTime;

namespace EduWork.WebApi.Controllers
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTimesController : ControllerBase
    {
        private readonly UserProjectTimeService _userProjectTimeService;

        public ProjectTimesController(UserProjectTimeService userProjectTimeService)
        {
            _userProjectTimeService = userProjectTimeService;
        }

        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<ProjectSmallDto>>> GetProjects([FromServices] IIdentity currentUser)
        {
            var result = await _userProjectTimeService.GetProjects(currentUser.Email);
            return Ok(result);
        }

        [HttpGet("usernames")]
        public async Task<ActionResult<IEnumerable<ProjectSmallDto>>> GetUsernames()
        {
            var result = await _userProjectTimeService.GetUsernames();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTimeDtoTest>>> GetMyProjectTimes([FromServices] IIdentity currentUser, [FromQuery] DateTime userWorkDay)
        {
            var result = await _userProjectTimeService.GetMyProjectTimes(currentUser.Email, userWorkDay);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> InputProjectTime([FromServices] IIdentity currentUser, ProjectTimeRequestDto projectTime)
        {
            await _userProjectTimeService.InputProjectTime(currentUser.Email, projectTime);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProjectTime([FromServices] IIdentity currentUser, int id, ProjectTimeRequestDto projectTime)
        {
            await _userProjectTimeService.UpdateProjectTime(currentUser.Email, id, projectTime);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectTime(int id)
        {
            await _userProjectTimeService.DeleteProjectTime(id);
            return Ok();
        }

        [HttpGet]
        [Route("statsFilter/admin")]
        public async Task<ActionResult<ProjectTimeResponseDto>> GetProjectTimesFilter([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? username, [FromQuery] string? projectTitle)
        {
            var result = await _userProjectTimeService.GetProjectTimesFilter(fromDate, toDate, username, projectTitle);
            return Ok(result);
        }

        [HttpGet]
        [Route("statsFilter")]
        public async Task<ActionResult<ProjectTimeResponseDto>> GetMyProjectTimesFilter([FromServices] IIdentity currentUser, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? projectTitle)
        {
            var result = await _userProjectTimeService.GetMyProjectTimesFilter(currentUser.Email, fromDate, toDate, projectTitle);
            return Ok(result);
        }

        [HttpGet]
        [Route("historyFilter")]
        public async Task<ActionResult<ProjectTimeHistoryDto>> GetMyHistoryProjectTimesFilter([FromServices] IIdentity currentUser, [FromQuery] bool? thisMonth, [FromQuery] bool? lastMonth, [FromQuery] bool? thisQuarter)
        {
            var result = await _userProjectTimeService.GetMyHistoryProjectTimesFilter(currentUser.Email, thisMonth, lastMonth, thisQuarter);
            return Ok(result);
        }

        [HttpGet]
        [Route("historyFilter/admin")]
        public async Task<ActionResult<ProjectTimeHistoryDto>> GetHistoryProjectTimesFilter([FromQuery] bool? thisMonth, [FromQuery] bool? lastMonth, [FromQuery] bool? thisQuarter, [FromQuery] string? username)
        {
            var result = await _userProjectTimeService.GetHistoryProjectTimesFilter(thisMonth, lastMonth, thisQuarter, username);
            return Ok(result);
        }
    }
}
