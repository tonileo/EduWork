using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using EduWork.BusinessLayer.Contracts;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EduWork.BusinessLayer.Services
{
    public class UserProjectTimeService : IUserProjectTimeService
    {
        private readonly AppDbContext _context;

        public UserProjectTimeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectTimeDto> AddProjectTime(ProjectTimeDto projectTime)
        {
            try
            {
                var dateToday = DateOnly.FromDateTime(DateTime.Now);
                //var dateToday = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

                var userWorkDayId = await _context.WorkDays.Where(d => d.WorkDate == dateToday).Select(s => s.Id).FirstOrDefaultAsync();
                if (userWorkDayId == 0)
                {
                    //return null;
                    throw new ArgumentException("Work day is not generated for today");
                }

                var projectExists = await _context.Projects.AnyAsync(d => d.Id == projectTime.ProjectId);
                if (!projectExists)
                {
                    //return null;
                    throw new ArgumentException("Project with that Id doesn't exist");
                }

                //var projectIsPayable = await context.Projects.Where(d => d.Id == projectTime.ProjectId).Select(s => s.IsPayable).FirstOrDefaultAsync();

                var userProjectTime = new ProjectTime()
                {
                    Id = projectTime.Id,
                    Comment = projectTime.Comment,
                    TimeSpentMinutes = projectTime.TimeSpentMinutes,
                    WorkDayId = userWorkDayId,
                    ProjectId = projectTime.ProjectId
                };

                await _context.ProjectTimes.AddAsync(userProjectTime);
                await _context.SaveChangesAsync();

                return projectTime;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Problem with adding project time" + ex);
            }
        }
    }
}
