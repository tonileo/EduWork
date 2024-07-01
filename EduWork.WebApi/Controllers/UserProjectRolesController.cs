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
    public class UserProjectRolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserProjectRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProjectRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProjectRole>>> GetUserProjectRoles()
        {
            return await _context.UserProjectRoles.ToListAsync();
        }

        // GET: api/UserProjectRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProjectRole>> GetUserProjectRole(int id)
        {
            var userProjectRole = await _context.UserProjectRoles.FindAsync(id);

            if (userProjectRole == null)
            {
                return NotFound();
            }

            return userProjectRole;
        }

        // PUT: api/UserProjectRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProjectRole(int id, UserProjectRole userProjectRole)
        {
            if (id != userProjectRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(userProjectRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProjectRoleExists(id))
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

        // POST: api/UserProjectRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProjectRole>> PostUserProjectRole(UserProjectRole userProjectRole)
        {
            _context.UserProjectRoles.Add(userProjectRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserProjectRole", new { id = userProjectRole.Id }, userProjectRole);
        }

        // DELETE: api/UserProjectRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProjectRole(int id)
        {
            var userProjectRole = await _context.UserProjectRoles.FindAsync(id);
            if (userProjectRole == null)
            {
                return NotFound();
            }

            _context.UserProjectRoles.Remove(userProjectRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProjectRoleExists(int id)
        {
            return _context.UserProjectRoles.Any(e => e.Id == id);
        }
    }
}
