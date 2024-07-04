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

        public async Task<ProjectTimeResponseDto> InputProjectTime(ProjectTimeDto projectTime)
        {
            var response = new ProjectTimeResponseDto();

            try
            {
                if (projectTime.TimeSpentMinutes == 0)
                {
                    response.Success = false;
                    response.Message = "TimeSpentMinutes can't be 0";
                    return response;
                }

                var dateToday = DateOnly.FromDateTime(DateTime.Now);

                var userWorkDayId = await _context.WorkDays.Where(d => d.WorkDate == dateToday).Select(s => s.Id).FirstOrDefaultAsync();
                if (userWorkDayId == 0)
                {
                    response.Success = false;
                    response.Message = "Work day is not generated for today";
                    return response;
                    //return null;
                    //throw new ArgumentException("Work day is not generated for today");
                }

                var projectIsPayable = await _context.Projects.Where(d => d.Id == projectTime.ProjectId).Select(s => s.IsPayable).FirstOrDefaultAsync();
                if (!projectIsPayable)
                {
                    response.Success = false;
                    response.Message = "Project with that Id doesn't exist";
                    return response;
                    //return null;
                    //throw new ArgumentException("Project with that Id doesn't exist");
                }

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

                //return userProjectTime;

                response.Success = true;
                response.ProjectTimeDto = projectTime;
                response.ProjectIsPayable = projectIsPayable;
                return response;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Problem with inputting project time" + ex);
            }
        }

        public async Task<List<ProjectTimeDto>> GetProjectTimes()
        {
            var userprojectTimes = await _context.ProjectTimes.ToListAsync();

            var userProfiles = new List<ProjectTimeDto>();
            foreach (var userprojectTime in userprojectTimes)
            {
                userProfiles.Add(new ProjectTimeDto()
                {
                    Id = userprojectTime.Id,
                    Comment = userprojectTime.Comment,
                    TimeSpentMinutes = userprojectTime.TimeSpentMinutes
                });
            }
            return userProfiles;
        }

        public async Task<List<ProjectTimeDto>> GetProjectTimesFilter(string username, string projectTitle)
        {
            List<ProjectTime> projectTimes = new List<ProjectTime>();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(projectTitle))
            {
                projectTimes = await _context.Users
                    .Where(u => u.Username == username)
                    .SelectMany(u => u.WorkDays)
                    .SelectMany(wd => wd.ProjectTimes)
                    .Where(pt => pt.Project.Title == projectTitle)
                    .ToListAsync();
            }
            else if (string.IsNullOrEmpty(username))
            {
                projectTimes = await _context.Projects
                    .Where(s => s.Title == projectTitle)
                    .SelectMany(a => a.ProjectTimes)
                    .ToListAsync();
            }
            else if (string.IsNullOrEmpty(projectTitle))
            {
                projectTimes = await _context.Users
                    .Where(s => s.Username == username)
                    .SelectMany(wd => wd.WorkDays)
                    .SelectMany(a => a.ProjectTimes)
                    .ToListAsync();
            }

            var userProjectTimes = projectTimes.Select(pt => new ProjectTimeDto
            {
                Id = pt.Id,
                Comment = pt.Comment,
                TimeSpentMinutes = pt.TimeSpentMinutes
            }).ToList();

            return userProjectTimes;
        }

        //public async Task<List<ProjectTimeDto>> GetUserProjectTimes(string username)
        //{
        //    if (string.IsNullOrEmpty(username))
        //    {
        //        return null;
        //    }

        //    var userWorkDays = await _context.Users.Where(s => s.Username == username)
        //        .SelectMany(wd => wd.WorkDays).SelectMany(a => a.ProjectTimes).ToListAsync();

        //    var userProjectTimes = userWorkDays.Select(pt => new ProjectTimeDto
        //    {
        //        Id = pt.Id,
        //        Comment = pt.Comment,
        //        TimeSpentMinutes = pt.TimeSpentMinutes
        //    }).ToList();

        //    return userProjectTimes;
        //}

        //public async Task<List<ProjectTimeDto>> GetUserProjectTimes(int id)
        //{
        //    var userWorkDays = await _context.WorkDays.Where(s => s.UserId == id).SelectMany(wd => wd.ProjectTimes).ToListAsync();

        //    var userProjectTimes = userWorkDays.Select(pt => new ProjectTimeDto
        //    {
        //        Id = pt.Id,
        //        Comment = pt.Comment,
        //        TimeSpentMinutes = pt.TimeSpentMinutes
        //    }).ToList();

        //    return userProjectTimes;
        //}
    }
}
