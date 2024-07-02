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
    public class ProjectRolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectRole>>> GetProjectRoles()
        {
            return await _context.ProjectRoles.ToListAsync();
        }

        // GET: api/ProjectRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectRole>> GetProjectRole(int id)
        {
            var projectRole = await _context.ProjectRoles.FindAsync(id);

            if (projectRole == null)
            {
                return NotFound();
            }

            return projectRole;
        }

        // PUT: api/ProjectRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectRole(int id, ProjectRole projectRole)
        {
            if (id != projectRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectRoleExists(id))
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

        // POST: api/ProjectRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectRole>> PostProjectRole(ProjectRole projectRole)
        {
            _context.ProjectRoles.Add(projectRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectRole", new { id = projectRole.Id }, projectRole);
        }

        // DELETE: api/ProjectRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectRole(int id)
        {
            var projectRole = await _context.ProjectRoles.FindAsync(id);
            if (projectRole == null)
            {
                return NotFound();
            }

            _context.ProjectRoles.Remove(projectRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectRoleExists(int id)
        {
            return _context.ProjectRoles.Any(e => e.Id == id);
        }
    }
}
