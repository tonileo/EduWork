using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using EduWork.BusinessLayer.Contracts;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EduWork.BusinessLayer.Services
{
    public class UserProjectTimeService(AppDbContext context, IMapper mapper) : IUserProjectTimeService
    {
        public async Task InputProjectTime(ProjectTimeRequestDto projectTime)
        {
            try
            {
                if (projectTime.TimeSpentMinutes == 0)
                {
                    throw new ArgumentException("TimeSpentMinutes can't be 0");
                }

                var dateToday = DateOnly.FromDateTime(DateTime.Now);

                var userWorkDayId = await context.WorkDays.Where(d => d.WorkDate == dateToday).Select(s => s.Id).FirstOrDefaultAsync();
                if (userWorkDayId == 0)
                {
                    throw new ArgumentException("Work day is not generated for today");
                }

                var projectIsPayable = await context.Projects.Where(d => d.Id == projectTime.ProjectId).Select(s => s.IsPayable).FirstOrDefaultAsync();
                if (!projectIsPayable)
                {
                    throw new ArgumentException("Project with that Id doesn't exist");
                }

                var userProjectTime = mapper.Map<ProjectTime>(projectTime);
                userProjectTime.WorkDayId = userWorkDayId;

                await context.ProjectTimes.AddAsync(userProjectTime);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Problem with inputting project time" + ex);
            }
        }

        public async Task<List<ProjectTimeDtoTest>> GetProjectTimes() //only for testing, will remove later
        {
            var userprojectTimes = await context.ProjectTimes.AsNoTracking().ToListAsync();

            var userProfiles = mapper.Map<List<ProjectTimeDtoTest>>(userprojectTimes);

            return userProfiles;
        }

        public async Task<List<ProjectTimeDto>> GetProjectTimesFilter(string? fromDate, string? toDate, string username, string projectTitle)
        {
            IQueryable<ProjectTime> query = context.ProjectTimes.Include(k => k.Project).AsQueryable();

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                if (DateOnly.TryParse(fromDate, out DateOnly parsedFromDate) && DateOnly.TryParse(toDate, out DateOnly parsedToDate))
                {
                    query = query.Where(pt => pt.WorkDay.WorkDate >= parsedFromDate && pt.WorkDay.WorkDate <= parsedToDate);
                }
            }

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(pt => pt.WorkDay.User.Username == username);
            }

            if (!string.IsNullOrEmpty(projectTitle))
            {
                query = query.Where(pt => pt.Project.Title == projectTitle);
            }

            var projectTimes = await query.ToListAsync();

            var userProjectTimes = mapper.Map<List<ProjectTimeDto>>(projectTimes);

            return userProjectTimes;
        }

        public async Task<List<ProjectTimeDto>> GetMyProjectTimesFilter(string? email, string? fromDate, string? toDate, string projectTitle)
        {
            IQueryable<ProjectTime> query = context.ProjectTimes.Include(k => k.Project).Where(pt => pt.WorkDay.User.Email == email).AsQueryable();

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                if (DateOnly.TryParse(fromDate, out DateOnly parsedFromDate) && DateOnly.TryParse(toDate, out DateOnly parsedToDate))
                {
                    query = query.Where(pt => pt.WorkDay.WorkDate >= parsedFromDate && pt.WorkDay.WorkDate <= parsedToDate);
                }
            }

            if (!string.IsNullOrEmpty(projectTitle))
            {
                query = query.Where(pt => pt.Project.Title == projectTitle);
            }

            var projectTimes = await query.ToListAsync();

            var userProjectTimes = mapper.Map<List<ProjectTimeDto>>(projectTimes);

            return userProjectTimes;
        }
    }
}
