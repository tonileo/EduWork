using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.DTO.ProjectTime;
using EduWork.BusinessLayer.Contracts;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduWork.BusinessLayer.Services
{
    public class UserProjectTimeService(AppDbContext context, IMapper mapper) : IUserProjectTimeService
    {
        public async Task<List<ProjectSmallDto>> GetProjects(string? email)
        {
            var projects = await context.Projects
                .Where(p => !p.IsFinished)
                .AsNoTracking()
                .ToListAsync();

            var userProjectTime = await context.ProjectTimes
                .Include(s => s.Project)
                .Include(a => a.WorkDay)
                .Where(g => g.WorkDay.User.Email == email)
                .OrderByDescending(pt => pt.Id)
                .FirstOrDefaultAsync();

            var userProjects = mapper.Map<List<ProjectSmallDto>>(projects);

            foreach (var projectDto in userProjects)
            {
                projectDto.LastChosenTitle = userProjectTime?.Project.Title;
            }

            return userProjects;
        }

        public async Task<List<UsernamesDto>> GetUsernames()
        {
            var usernames = await context.Users.AsNoTracking().ToListAsync();

            var userprojects = mapper.Map<List<UsernamesDto>>(usernames);

            return userprojects;
        }

        public async Task<List<ProjectTimeDtoTest>> GetMyProjectTimes(string? email, DateTime userWorkDay)
        {
            if (userWorkDay > DateTime.Today)
            {
                throw new ArgumentException("Work day date can't be in future");
            }

            if (userWorkDay.DayOfWeek == DayOfWeek.Saturday || userWorkDay.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ArgumentException("Work day date can't be a weekend");
            }

            DateOnly userWorkDayDateOnly = DateOnly.FromDateTime(userWorkDay);

            var userprojectTimes = await context.ProjectTimes.Include(s => s.Project).Include(a => a.WorkDay).Where(g => g.WorkDay.User.Email == email && g.WorkDay.WorkDate == userWorkDayDateOnly).AsNoTracking().ToListAsync();

            var userProfiles = mapper.Map<List<ProjectTimeDtoTest>>(userprojectTimes);

            int sumTimeSpentHours = 0;
            int sumTimeSpentMinutes = 0;

            foreach (var projectTime in userprojectTimes)
            {
                sumTimeSpentHours += projectTime.TimeSpentMinutes / 60;
                sumTimeSpentMinutes += projectTime.TimeSpentMinutes % 60;
            }

            foreach (var userProfile in userProfiles)
            {
                userProfile.SumTimeSpentHours = sumTimeSpentHours;
                userProfile.SumTimeSpentMinutes = sumTimeSpentMinutes;
            }

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
                    var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email);

                    var newWorkDay = new WorkDay
                    {
                        WorkDate = projectTimeDateOnly,
                        UserId = user.Id,
                        User = user
                    };

                    await context.WorkDays.AddAsync(newWorkDay);
                    await context.SaveChangesAsync();

                    userWorkDayId = newWorkDay.Id;
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

        public async Task DeleteProjectTime(int id)
        {
            try
            {
                var existingProjectTime = await context.ProjectTimes.FindAsync(id);

                if (existingProjectTime == null)
                {
                    throw new ArgumentException("Project time entry not found");
                }
                else
                {
                    context.ProjectTimes.Remove(existingProjectTime);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problem with deleting project time" + ex.Message);
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
                    query = query.Where(pt => pt.Project.Title.Contains(projectTitle));
                }

                var projectTimes = await query.ToListAsync();

                var totalTimeSpentMinutes = projectTimes.Sum(pt => pt.TimeSpentMinutes);

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

                var projectTimeSums = projectTimes
                    .GroupBy(pt => pt.Project.Title)
                     .Select(g =>
                     {
                         var sumTimeSpent = g.Sum(pt => pt.TimeSpentMinutes);
                         var percentageTimeSpent = (int)Math.Round((double)sumTimeSpent / totalTimeSpentMinutes * 100);
                         return new ProjectTimeSumDto
                         {
                             TitleProject = g.Key,
                             SumTimeSpentHours = sumTimeSpent / 60,
                             SumTimeSpentMinutes = sumTimeSpent % 60,
                             PercentageTimeSpent = percentageTimeSpent,
                             IsEducation = projectProperties[g.Key].IsEducation,
                             IsFinished = projectProperties[g.Key].IsFinished,
                             IsPayable = projectProperties[g.Key].IsPayable,
                             IsPrivate = projectProperties[g.Key].IsPrivate
                         };
                     }).OrderByDescending(a => a.PercentageTimeSpent).ToList();

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
                    query = query.Where(pt => pt.Project.Title.Contains(projectTitle));
                }

                var projectTimes = await query.ToListAsync();

                var totalTimeSpentMinutes = projectTimes.Sum(pt => pt.TimeSpentMinutes);

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

                var projectTimeSums = projectTimes
                     .GroupBy(pt => pt.Project.Title)
                      .Select(g =>
                      {
                          var sumTimeSpent = g.Sum(pt => pt.TimeSpentMinutes);
                          var percentageTimeSpent = (int)Math.Round((double)sumTimeSpent / totalTimeSpentMinutes * 100);
                          return new ProjectTimeSumDto
                          {
                              TitleProject = g.Key,
                              SumTimeSpentHours = sumTimeSpent / 60,
                              SumTimeSpentMinutes = sumTimeSpent % 60,
                              PercentageTimeSpent = percentageTimeSpent,
                              IsEducation = projectProperties[g.Key].IsEducation,
                              IsFinished = projectProperties[g.Key].IsFinished,
                          };
                      }).OrderByDescending(a => a.PercentageTimeSpent).ToList();

                var projectTimeResponseDto = mapper.Map<List<ProjectTime>, ProjectTimeResponseDto>(projectTimes);

                projectTimeResponseDto.ProjectTimeSums = projectTimeSums.ToList();

                return projectTimeResponseDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problem with GetMyProjectTimesFilter" + ex.Message);
            }
        }

        public async Task<List<ProjectTimeHistoryDto>> GetMyHistoryProjectTimesFilter(string? email, bool? thisMonth, bool? lastMonth, bool? thisQuarter)
        {
            try
            {
                IQueryable<ProjectTime> query = context.ProjectTimes
                    .Include(k => k.Project)
                    .Where(pt => pt.WorkDay.User.Email == email)
                    .Include(w => w.WorkDay)
                    .AsNoTracking()
                    .AsQueryable();

                var startOfThisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var startOfLastMonth = startOfThisMonth.AddMonths(-1);
                var startOfNextMonth = startOfThisMonth.AddMonths(1);
                var startOfThisQuarter = new DateTime(DateTime.Now.Year, (DateTime.Now.Month - 3), 1);
                var startOfNextQuarter = startOfThisQuarter.AddMonths(3);

                DateOnly startOfThisMonthDateOnly = DateOnly.FromDateTime(startOfThisMonth);
                DateOnly startOfLastMonthDateOnly = DateOnly.FromDateTime(startOfLastMonth);
                DateOnly startOfNextMonthDateOnly = DateOnly.FromDateTime(startOfNextMonth);
                DateOnly startOfThisQuarterDateOnly = DateOnly.FromDateTime(startOfThisQuarter);
                DateOnly startOfNextQuarterDateOnly = DateOnly.FromDateTime(startOfNextQuarter);

                DateOnly startDate;
                DateOnly endDate;

                if (lastMonth == true)
                {
                    startDate = startOfLastMonthDateOnly;
                    endDate = startOfThisMonthDateOnly;
                }
                else if (thisQuarter == true)
                {
                    startDate = startOfThisQuarterDateOnly;
                    endDate = startOfNextQuarterDateOnly;
                }
                else
                {
                    startDate = startOfThisMonthDateOnly;
                    endDate = startOfNextMonthDateOnly;
                }

                query = query.Where(pt => pt.WorkDay.WorkDate >= startDate && pt.WorkDay.WorkDate < endDate);

                var projectTimes = await query.ToListAsync();

                var allDates = new List<DateOnly>();

                for (var date = startDate; date < endDate; date = date.AddDays(1))
                {
                    allDates.Add(date);
                }

                var groupedProjectTimes = projectTimes
                .GroupBy(pt => pt.WorkDay.WorkDate)
                .Select(g => new
                {
                    WorkDate = g.Key,
                    TotalTimeSpentMinutes = g.Sum(pt => pt.TimeSpentMinutes),
                    ProjectTimes = g.ToList()
                });

                var projectTimeHistoryDtos = new List<ProjectTimeHistoryDto>();

                var holidays = await context.NonWorkingDays.Select(h => h.NonWorkingDate).ToListAsync();

                foreach (var date in allDates)
                {
                    var groupedEntry = groupedProjectTimes.FirstOrDefault(g => g.WorkDate == date);

                    var totalTimeSpentMinutes = groupedEntry?.TotalTimeSpentMinutes ?? 0;
                    var timeSpentHours = totalTimeSpentMinutes / 60;
                    var timeSpentMinutes = totalTimeSpentMinutes % 60;

                    bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(date);

                    if (isWeekend)
                    {
                        var dto = new ProjectTimeHistoryDto
                        {
                            DateWorkDay = date,
                            SumTimeSpentHours = 0,
                            SumTimeSpentMinutes = 0,
                            IsNonWorkingDay = true,
                        };
                        projectTimeHistoryDtos.Add(dto);
                    }
                    else
                    {
                        if (groupedEntry != null)
                        {
                            foreach (var pt in groupedEntry.ProjectTimes)
                            {
                                var dto = mapper.Map<ProjectTimeHistoryDto>(pt);
                                dto.SumTimeSpentHours = timeSpentHours;
                                dto.SumTimeSpentMinutes = timeSpentMinutes;
                                dto.IsNonWorkingDay = false;
                                projectTimeHistoryDtos.Add(dto);
                            }
                        }
                        else
                        {
                            var dto = new ProjectTimeHistoryDto
                            {
                                DateWorkDay = date,
                                SumTimeSpentHours = timeSpentHours,
                                SumTimeSpentMinutes = timeSpentMinutes,
                                IsNonWorkingDay = false
                            };
                            projectTimeHistoryDtos.Add(dto);
                        }
                    }
                }
                return projectTimeHistoryDtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problem with GetMyProjectTimesFilter" + ex.Message);
            }
        }

        public async Task<List<ProjectTimeHistoryDto>> GetHistoryProjectTimesFilter(bool? thisMonth, bool? lastMonth, bool? thisQuarter, string? username)
        {
            try
            {
                IQueryable<ProjectTime> query = context.ProjectTimes.Include(k => k.Project)
                .Include(w => w.WorkDay)
                .Include(s => s.WorkDay.User)
                .AsNoTracking()
                .AsQueryable();

                if (!string.IsNullOrEmpty(username))
                {
                    query = query.Where(a => a.WorkDay.User.Username == username);

                    if (query.Count() == 0)
                    {
                        throw new ArgumentException("There is no users with that username");
                    }
                }

                var startOfThisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var startOfLastMonth = startOfThisMonth.AddMonths(-1);
                var startOfNextMonth = startOfThisMonth.AddMonths(1);
                var startOfThisQuarter = new DateTime(DateTime.Now.Year, (DateTime.Now.Month - 3), 1);
                var startOfNextQuarter = startOfThisQuarter.AddMonths(3);

                DateOnly startOfThisMonthDateOnly = DateOnly.FromDateTime(startOfThisMonth);
                DateOnly startOfLastMonthDateOnly = DateOnly.FromDateTime(startOfLastMonth);
                DateOnly startOfNextMonthDateOnly = DateOnly.FromDateTime(startOfNextMonth);
                DateOnly startOfThisQuarterDateOnly = DateOnly.FromDateTime(startOfThisQuarter);
                DateOnly startOfNextQuarterDateOnly = DateOnly.FromDateTime(startOfNextQuarter);

                DateOnly startDate;
                DateOnly endDate;

                if (lastMonth == true)
                {
                    startDate = startOfLastMonthDateOnly;
                    endDate = startOfThisMonthDateOnly;
                }
                else if (thisQuarter == true)
                {
                    startDate = startOfThisQuarterDateOnly;
                    endDate = startOfNextQuarterDateOnly;
                }
                else
                {
                    startDate = startOfThisMonthDateOnly;
                    endDate = startOfNextMonthDateOnly;
                }

                query = query.Where(pt => pt.WorkDay.WorkDate >= startDate && pt.WorkDay.WorkDate < endDate);

                var projectTimes = await query.ToListAsync();

                var allDates = new List<DateOnly>();

                for (var date = startDate; date < endDate; date = date.AddDays(1))
                {
                    allDates.Add(date);
                }

                var groupedProjectTimes = projectTimes
                .GroupBy(pt => pt.WorkDay.WorkDate)
                .Select(g => new
                {
                    WorkDate = g.Key,
                    TotalTimeSpentMinutes = g.Sum(pt => pt.TimeSpentMinutes),
                    ProjectTimes = g.ToList()
                });

                var projectTimeHistoryDtos = new List<ProjectTimeHistoryDto>();

                var holidays = await context.NonWorkingDays.Select(h => h.NonWorkingDate).ToListAsync();

                foreach (var date in allDates)
                {
                    var groupedEntry = groupedProjectTimes.FirstOrDefault(g => g.WorkDate == date);

                    var totalTimeSpentMinutes = groupedEntry?.TotalTimeSpentMinutes ?? 0;
                    var timeSpentHours = totalTimeSpentMinutes / 60;
                    var timeSpentMinutes = totalTimeSpentMinutes % 60;

                    bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(date);

                    if (isWeekend)
                    {
                        var dto = new ProjectTimeHistoryDto
                        {
                            DateWorkDay = date,
                            SumTimeSpentHours = 0,
                            SumTimeSpentMinutes = 0,
                            IsNonWorkingDay = true,
                        };
                        projectTimeHistoryDtos.Add(dto);
                    }
                    else
                    {
                        if (groupedEntry != null)
                        {
                            foreach (var pt in groupedEntry.ProjectTimes)
                            {
                                var dto = mapper.Map<ProjectTimeHistoryDto>(pt);
                                dto.SumTimeSpentHours = timeSpentHours;
                                dto.SumTimeSpentMinutes = timeSpentMinutes;
                                dto.IsNonWorkingDay = false;
                                projectTimeHistoryDtos.Add(dto);
                            }
                        }
                        else
                        {
                            var dto = new ProjectTimeHistoryDto
                            {
                                DateWorkDay = date,
                                SumTimeSpentHours = timeSpentHours,
                                SumTimeSpentMinutes = timeSpentMinutes,
                                IsNonWorkingDay = false
                            };
                            projectTimeHistoryDtos.Add(dto);
                        }
                    }
                }
                return projectTimeHistoryDtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problem with GetMyProjectTimesFilter" + ex.Message);
            }
        }
    }
}