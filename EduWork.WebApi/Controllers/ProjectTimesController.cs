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

namespace EduWork.WebApi.Controllers
{
    //[Authorize]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTimesController : ControllerBase
    {
        private readonly UserProjectTimeService _userProjectTimeService;

        public ProjectTimesController(UserProjectTimeService userProjectTimeService)
        {
            _userProjectTimeService = userProjectTimeService;
        }

        [HttpPost]
        public async Task<ActionResult> InputProjectTime(ProjectTimeRequestDto projectTime)
        {
            await _userProjectTimeService.InputProjectTime(projectTime);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTimeDto>>> GetProjectTimes()
        {
            return await _userProjectTimeService.GetProjectTimes();
        }

        [HttpGet]
        [Route("filter")]
        public async Task<ActionResult<IEnumerable<ProjectTimeDto>>> GetProjectTimesFilter([FromQuery] string? username, [FromQuery] string? projectTile)
        {
            return await _userProjectTimeService.GetProjectTimesFilter(username, projectTile);
        }

        //[HttpGet("{username}")]
        //public async Task<ActionResult<IEnumerable<ProjectTimeDto>>> GetUserProjectTimes(string username)
        //{
        //    return await _userProjectTimeService.GetUserProjectTimes(username) ?? (ActionResult<IEnumerable<ProjectTimeDto>>)NotFound();
        //}

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
