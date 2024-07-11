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
        public async Task<List<ProjectSmallDto>> GetProjects() //will switch to differnet controller later
        {
            var projects = await context.Projects.AsNoTracking().ToListAsync();

            var userprojects = mapper.Map<List<ProjectSmallDto>>(projects);

            return userprojects;
        }

        public async Task<List<ProjectTimeDtoTest>> GetProjectTimes() //only for testing, will remove later
        {
            var userprojectTimes = await context.ProjectTimes.AsNoTracking().ToListAsync();

            var userProfiles = mapper.Map<List<ProjectTimeDtoTest>>(userprojectTimes);

            return userProfiles;
        }

        public async Task InputProjectTime(string? email, ProjectTimeRequestDto projectTime)
        {
            try
            {
                if (email == null)
                {
                    throw new ArgumentException("User not logged in");
                }

                if (projectTime.TimeSpentMinutes <= 0)
                {
                    throw new ArgumentException("TimeSpentMinutes can't be 0 or less then 0");
                }

                DateOnly projectTimeDateOnly = DateOnly.FromDateTime(projectTime.DateWorkDay);

                var userWorkDayId = await context.WorkDays
                    .Include(g => g.User)
                    .Where(d => d.WorkDate == projectTimeDateOnly && d.User.Email == email)
                    .Select(s => s.Id)
                    .SingleOrDefaultAsync();

                if (userWorkDayId == 0)
                {
                    throw new ArgumentException("Work day for logged in user is not generated");
                }

                var projectExist = await context.Projects
                    .Where(d => d.Title == projectTime.TitleProject)
                    .Select(s => s.Id)
                    .FirstOrDefaultAsync();

                if (projectExist == 0)
                {
                    throw new ArgumentException("Project with that Id doesn't exist");
                }

                var userProjectTime = mapper.Map<ProjectTime>(projectTime);
                userProjectTime.WorkDayId = userWorkDayId;
                userProjectTime.ProjectId = projectExist;

                await context.ProjectTimes.AddAsync(userProjectTime);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problem with inputting project time" + ex.Message);
            }
        }

        public async Task UpdateProjectTime(string? email, int id, ProjectTimeRequestDto projectTime)
        {
            try
            {
                if (email == null)
                {
                    throw new ArgumentException("User not logged in");
                }

                if (projectTime.TimeSpentMinutes <= 0)
                {
                    throw new ArgumentException("TimeSpentMinutes can't be 0 or less then 0");
                }

                var existingProjectTime = await context.ProjectTimes.FindAsync(id);
                if (existingProjectTime == null)
                {
                    throw new ArgumentException("Project time entry not found");
                }

                var projectExist = await context.Projects
                    .Where(d => d.Title == projectTime.TitleProject)
                    .Select(s => s.Id)
                    .FirstOrDefaultAsync();

                if (projectExist == 0)
                {
                    throw new ArgumentException("Project with that Id doesn't exist");
                }

                DateOnly projectTimeDateOnly = DateOnly.FromDateTime(projectTime.DateWorkDay);

                var wordDayExist = await context.WorkDays
                    .Where(s => s.WorkDate == projectTimeDateOnly)
                    .Select(f => f.Id)
                    .FirstOrDefaultAsync();

                if (wordDayExist == 0)
                {
                    throw new ArgumentException("Work day with that Id doesn't exist");
                }

                existingProjectTime.ProjectId = projectExist;
                existingProjectTime.TimeSpentMinutes = projectTime.TimeSpentMinutes;
                existingProjectTime.Comment = projectTime.Comment;
                existingProjectTime.WorkDayId = wordDayExist;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problem with updating project time" + ex.Message);
            }
        }

        public async Task<ProjectTimeResponseDto> GetProjectTimesFilter(DateTime? fromDate, DateTime? toDate, string? username, string? projectTitle)
        {
            try
            {
                IQueryable<ProjectTime> query = context.ProjectTimes
                .Include(k => k.Project).AsNoTracking().AsQueryable();

                if (fromDate != null && toDate != null)
                {
                    DateOnly fromDateOnly = DateOnly.FromDateTime((DateTime)fromDate);
                    DateOnly toDateOnly = DateOnly.FromDateTime((DateTime)toDate);

                    query = query.Where(pt => pt.WorkDay.WorkDate >= fromDateOnly && pt.WorkDay.WorkDate <= toDateOnly);
                }
                else if (fromDate != null)
                {
                    DateOnly fromDateOnly = DateOnly.FromDateTime((DateTime)fromDate);

                    query = query.Where(pt => pt.WorkDay.WorkDate >= fromDateOnly);
                }
                else if (toDate != null)
                {
                    DateOnly toDateOnly = DateOnly.FromDateTime((DateTime)toDate);

                    query = query.Where(pt => pt.WorkDay.WorkDate <= toDateOnly);
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

                var projectTitles = projectTimes.Select(pt => pt.Project.Title).Distinct().ToList();
                var projectProperties = await context.Projects
                    .Where(p => projectTitles.Contains(p.Title))
                    .Select(p => new
                    {
                        p.Title,
                        p.IsEducation,
                        p.IsFinished,
                        p.IsPayable,
                        p.IsPrivate
                    })
                    .ToDictionaryAsync(p => p.Title);

                var projectTimeSumsTasks = projectTimes
                    .GroupBy(pt => pt.Project.Title)
                    .Select(async g => new ProjectTimeSumDto
                    {
                        TitleProject = g.Key,
                        SumTimeSpent = g.Sum(pt => pt.TimeSpentMinutes),
                        IsEducation = projectProperties[g.Key].IsEducation,
                        IsFinished = projectProperties[g.Key].IsFinished,
                        IsPayable = projectProperties[g.Key].IsPayable,
                        IsPrivate = projectProperties[g.Key].IsPrivate
                    });

                var projectTimeSums = await Task.WhenAll(projectTimeSumsTasks);

                var projectTimeResponseDto = mapper.Map<List<ProjectTime>, ProjectTimeResponseDto>(projectTimes);

                projectTimeResponseDto.ProjectTimeSums = projectTimeSums.ToList();

                return projectTimeResponseDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problem with GetProjectTimesFilter" + ex.Message);
            }
        }

        public async Task<ProjectTimeResponseDto> GetMyProjectTimesFilter(string? email, DateTime? fromDate, DateTime? toDate, string? projectTitle)
        {
            try
            {
                IQueryable<ProjectTime> query = context.ProjectTimes.Include(k => k.Project)
                .Where(pt => pt.WorkDay.User.Email == email).AsNoTracking().AsQueryable();

                if (fromDate != null && toDate != null)
                {
                    DateOnly fromDateOnly = DateOnly.FromDateTime((DateTime)fromDate);
                    DateOnly toDateOnly = DateOnly.FromDateTime((DateTime)toDate);

                    query = query.Where(pt => pt.WorkDay.WorkDate >= fromDateOnly && pt.WorkDay.WorkDate <= toDateOnly);
                }
                else if (fromDate != null)
                {
                    DateOnly fromDateOnly = DateOnly.FromDateTime((DateTime)fromDate);

                    query = query.Where(pt => pt.WorkDay.WorkDate >= fromDateOnly);
                }
                else if (toDate != null)
                {
                    DateOnly toDateOnly = DateOnly.FromDateTime((DateTime)toDate);

                    query = query.Where(pt => pt.WorkDay.WorkDate <= toDateOnly);
                }

                if (!string.IsNullOrEmpty(projectTitle))
                {
                    query = query.Where(pt => pt.Project.Title == projectTitle);
                }

                var projectTimes = await query.ToListAsync();

                var projectTitles = projectTimes.Select(pt => pt.Project.Title).Distinct().ToList();
                var projectProperties = await context.Projects
                    .Where(p => projectTitles.Contains(p.Title))
                    .Select(p => new
                    {
                        p.Title,
                        p.IsEducation,
                        p.IsFinished,
                        p.IsPayable,
                        p.IsPrivate
                    })
                    .ToDictionaryAsync(p => p.Title);

                var projectTimeSumsTasks = projectTimes
                    .GroupBy(pt => pt.Project.Title)
                    .Select(async g => new ProjectTimeSumDto
                    {
                        TitleProject = g.Key,
                        SumTimeSpent = g.Sum(pt => pt.TimeSpentMinutes),
                        IsEducation = projectProperties[g.Key].IsEducation,
                        IsFinished = projectProperties[g.Key].IsFinished,
                        IsPayable = projectProperties[g.Key].IsPayable,
                        IsPrivate = projectProperties[g.Key].IsPrivate
                    });

                var projectTimeSums = await Task.WhenAll(projectTimeSumsTasks);

                var projectTimeResponseDto = mapper.Map<List<ProjectTime>, ProjectTimeResponseDto>(projectTimes);

                projectTimeResponseDto.ProjectTimeSums = projectTimeSums.ToList();

                return projectTimeResponseDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problem with GetMyProjectTimesFilter" + ex.Message);
            }
        }
    }
}