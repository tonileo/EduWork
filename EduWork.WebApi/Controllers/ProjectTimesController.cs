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
using Common.DTO;
using EduWork.WebApi.Authentication;

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
        public async Task<ActionResult<IEnumerable<ProjectSmallDto>>> GetProjects()
        {
            var result = await _userProjectTimeService.GetProjects();
            return Ok(result);
        }

        [HttpGet("usernames")]
        public async Task<ActionResult<IEnumerable<ProjectSmallDto>>> GetUsernames()
        {
            var result = await _userProjectTimeService.GetUsernames();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTimeDtoTest>>> GetMyProjectTimes([FromServices] IIdentity currentUser)
        {
            var result = await _userProjectTimeService.GetMyProjectTimes(currentUser.Email);
            return Ok(result);
        }

        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<ProjectTimeDtoTest>>> GetProjectTimes()
        {
            var result = await _userProjectTimeService.GetProjectTimes();
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

        [HttpGet]
        [Route("adminFilter")]
        public async Task<ActionResult<ProjectTimeResponseDto>> GetProjectTimesFilter([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? username, [FromQuery] string? projectTitle)
        {
            var result = await _userProjectTimeService.GetProjectTimesFilter(fromDate, toDate, username, projectTitle);
            return Ok(result);
        }

        [HttpGet]
        [Route("userFilter")]
        public async Task<ActionResult<ProjectTimeResponseDto>> GetMyProjectTimesFilter([FromServices] IIdentity currentUser, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? projectTitle)
        {
            var result = await _userProjectTimeService.GetMyProjectTimesFilter(currentUser.Email, fromDate, toDate, projectTitle);
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<IEnumerable<ProjectTimeDto>>> GetUserProjectTimes(int id)
        //{
        //    var projectTime = await _userProjectTimeService.GetUserProjectTimes(id);

        //    if (projectTime == null)
        //    {
        //        return NotFound();
        //    }

        //    return projectTime;
        //}

        //// PUT: api/ProjectTimes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProjectTime(int id, ProjectTime projectTime)
        //{
        //    if (id != projectTime.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(projectTime).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectTimeExists(id))
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

        // DELETE: api/ProjectTimes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProjectTime(int id)
        //{
        //    var projectTime = await _context.ProjectTimes.FindAsync(id);
        //    if (projectTime == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ProjectTimes.Remove(projectTime);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ProjectTimeExists(int id)
        //{
        //    return _context.ProjectTimes.Any(e => e.Id == id);
        //}
    }
}
