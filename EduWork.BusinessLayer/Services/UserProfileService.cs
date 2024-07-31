using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO.Profile;
using Common.DTO.ProjectTime;
using EduWork.BusinessLayer.Contracts;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace EduWork.BusinessLayer.Services
{
    public class UserProfileService(AppDbContext context, IMapper mapper) : IUserProfileService
    {
        public async Task<CurrentUserProjectDto> GetUserCurrentProject(int id)
        {
            var userProjectTime = await context.ProjectTimes
                .Include(s => s.Project)
                .Include(a => a.WorkDay)
                .Where(g => g.WorkDay.User.Id == id)
                .OrderByDescending(pt => pt.WorkDay.WorkDate)
                .Select(m => m.Project.Title)
                .FirstOrDefaultAsync();

            var userProjectTimeRole = await context.UserProjectRoles
                .Include(s => s.User)
                .Include(u => u.ProjectRole)
                .Where(g => g.User.Id == id)
                .Where(b => b.Project.Title == userProjectTime)
                .Select(m => m.ProjectRole.Title)
                .FirstOrDefaultAsync();

            var userProjects = new CurrentUserProjectDto
            {
                Title = userProjectTime,
                Role = userProjectTimeRole
            };

            return userProjects;
        }

        public async Task<List<ProjectsProfileDto>> GetProjects()
        {
            var projects = await context.Projects.AsNoTracking().ToListAsync();

            var projectsProfile = mapper.Map<List<ProjectsProfileDto>>(projects);

            return projectsProfile;
        }

        public async Task<List<UserProfileDto>> GetUserSmallProfiles(string? username, bool? asc)
        {
            var query = context.Users.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(u => u.Username.Contains(username));
            }

            if (asc != null && asc != true)
            {
                query = query.OrderByDescending(u => u.Username);
            }
            else
            {
                query = query.OrderBy(u => u.Username);
            }

            var users = await query.ToListAsync();

            var profiles = new List<UserProfileDto>();

            foreach (var user in users)
            {
                var project = await GetUserCurrentProject(user.Id);

                var userProfile = mapper.Map<UserProfileDto>(user);
                userProfile.Project = project;

                profiles.Add(userProfile);
            }

            return profiles;
        }

        public async Task<List<SickLeaveRecordDto>> GetUserSickLeaveRecords(int id)
        {
            var sickLeaveRecord = await context.SickLeaveRecords.Where(s => s.UserId == id).AsNoTracking().ToListAsync();

            var userSickLeave = new List<SickLeaveRecordDto>();

            foreach (var record in sickLeaveRecord)
            {
                var totalSickDays = (record.EndDate.ToDateTime(new TimeOnly()) - record.StartDate.ToDateTime(new TimeOnly())).Days + 1;

                var sickLeaveDto = mapper.Map<SickLeaveRecordDto>(record);
                sickLeaveDto.TotalSickDays = totalSickDays;

                userSickLeave.Add(sickLeaveDto);
            }

            return userSickLeave;
        }

        public async Task<List<AnnualLeaveDto>> GetUserAnnualLeaves(int id)
        {
            var annualLeave = await context.AnnualLeaves.Where(s => s.UserId == id).AsNoTracking().ToListAsync();

            var userAnnualLeave = mapper.Map<List<AnnualLeaveDto>>(annualLeave);

            return userAnnualLeave;
        }

        public async Task<List<AnnualLeaveRecordDto>> GetUserAnnualLeaveRecords(int id)
        {
            var annualLeave = await context.AnnualLeavesRecords.Where(s => s.UserId == id).AsNoTracking().ToListAsync();

            var userAnnualLeave = mapper.Map<List<AnnualLeaveRecordDto>>(annualLeave);

            return userAnnualLeave;
        }

        public async Task<MyProfileDto> GetUserProfile(string? username)
        {
            var user = await context.Users.Where(u => u.Username == username).Include(a => a.AppRole).AsNoTracking().SingleOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("User doesn't exist");
            }

            var projects = await GetUserCurrentProject(user.Id);
            var sickLeaveRecords = await GetUserSickLeaveRecords(user.Id);
            var annualLeaves = await GetUserAnnualLeaves(user.Id);
            var annualLeave = from al in annualLeaves where al.Year == DateTime.Now.Year select al;
            var annualLeaveRecords = await GetUserAnnualLeaveRecords(user.Id);

            var userProfile = new MyProfileDto()
            {
                Id = user.Id,
                Username = user.Username,
                AppRoleTitle = user.AppRole.Title,
                Email = user.Email,
                Project = projects,
                AnnualLeave = annualLeave.FirstOrDefault(),
                AnnualLeaveRecords = annualLeaveRecords,
                SickLeaveRecords = sickLeaveRecords
            };

            return userProfile;
        }

        public async Task<MyProfileStatsDto> GetMyProfileStats(string? username, bool thisMonth, bool lastMonth)
        {
            IQueryable<ProjectTime> query = context.ProjectTimes
                .Include(k => k.Project)
                .Where(pt => pt.WorkDay.User.Username == username)
                .AsNoTracking()
                .AsQueryable();

            var startOfThisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var startOfLastMonth = startOfThisMonth.AddMonths(-1);
            var startOfNextMonth = startOfThisMonth.AddMonths(1);

            DateOnly startOfThisMonthDateOnly = DateOnly.FromDateTime(startOfThisMonth);
            DateOnly startOfLastMonthDateOnly = DateOnly.FromDateTime(startOfLastMonth);
            DateOnly startOfNextMonthDateOnly = DateOnly.FromDateTime(startOfNextMonth);

            DateOnly startDate;
            DateOnly endDate;

            if (lastMonth)
            {
                startDate = startOfLastMonthDateOnly;
                endDate = startOfThisMonthDateOnly;
            }
            else
            {
                startDate = startOfThisMonthDateOnly;
                endDate = startOfNextMonthDateOnly;
            }

            query = query.Where(pt => pt.WorkDay.WorkDate >= startDate && pt.WorkDay.WorkDate < endDate);

            var projectTimes = await query.ToListAsync();

            var sumProjectTimes = projectTimes.Sum(s => s.TimeSpentMinutes);
            var countProjectTimes = projectTimes.Select(pt => pt.Project.Id).Distinct().Count();

            var userProfileStats = new MyProfileStatsDto()
            {
                SumHoursSpent = sumProjectTimes / 60,
                SumMinutesSpent = sumProjectTimes % 60,
                CountProjects = countProjectTimes
            };

            return userProfileStats;
        }

        public async Task AddAnnualLeave(string? username, ProfileAnnualRequestDto annualLeave)
        {
            if (annualLeave.StartDate <= DateTime.Today || annualLeave.EndDate <= DateTime.Today)
            {
                throw new ArgumentException("Can't request today and days before today as annual leave");
            }

            if (annualLeave.StartDate > annualLeave.EndDate)
            {
                throw new ArgumentException("Start date can't be after end date");
            }

            var user = await context.Users
                .Include(u => u.AnnualLeaveRecords)
                .Include(a => a.AnnualLeaves)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var currentYear = DateTime.Now.Year;
            var dateAvailability = user.AnnualLeaves
                .SingleOrDefault(al => al.Year == currentYear);

            if (dateAvailability == null)
            {
                throw new InvalidOperationException("Annual leave record not found");
            }

            var annualLeaveStartDate = DateOnly.FromDateTime(annualLeave.StartDate);
            var annualLeaveEndDate = DateOnly.FromDateTime(annualLeave.EndDate);
            var dateOverlap = user.AnnualLeaveRecords
                .Where(record => annualLeaveStartDate <= record.EndDate && annualLeaveEndDate >= record.StartDate)
                .ToList();

            if (dateOverlap.Any())
            {
                throw new InvalidOperationException("Requested dates overlap with existing sick leave records");
            }

            var nonWorkingDays = await context.NonWorkingDays
                .AsNoTracking()
                .Select(u => u.NonWorkingDate)
                .ToListAsync();

            var difference = CountWorkingDays(annualLeave.StartDate, annualLeave.EndDate, nonWorkingDays);

            if (difference > dateAvailability.LeftLeaveDays)
            {
                throw new ArgumentException("Don't have that many days available");
            }

            var newLeftLeaveDays = dateAvailability.LeftLeaveDays - difference;

            var addAnnual = mapper.Map<AnnualLeaveRecord>(annualLeave);
            addAnnual.UserId = dateAvailability.UserId;

            await context.AddAsync(addAnnual);

            dateAvailability.Year = DateTime.Now.Year;
            dateAvailability.LeftLeaveDays = newLeftLeaveDays;

            await context.SaveChangesAsync();
        }

        public async Task AddSickDay(string? username, ProfileAnnualRequestDto annualLeave)
        {
            if (annualLeave.StartDate > annualLeave.EndDate)
            {
                throw new ArgumentException("Start date can't be after end date");
            }

            var user = await context.Users
                .Include(u => u.SickLeaveRecords)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var annualLeaveStartDate = DateOnly.FromDateTime(annualLeave.StartDate);
            var annualLeaveEndDate = DateOnly.FromDateTime(annualLeave.EndDate);

            var dateOverlap = user.SickLeaveRecords
                    .Where(record => (annualLeaveStartDate <= record.EndDate && annualLeaveEndDate >= record.StartDate))
                    .ToList();

            if (dateOverlap.Any())
            {
                throw new InvalidOperationException("Requested dates overlap with existing sick leave records");
            }

            var addAnnual = mapper.Map<SickLeaveRecord>(annualLeave);
            addAnnual.UserId = user.Id;

            await context.AddAsync(addAnnual);
            await context.SaveChangesAsync();
        }

        private static int CountWorkingDays(DateTime startDate, DateTime endDate, List<DateOnly> nonWorkingDays)
        {
            int workingDays = 0;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                var dateOnly = DateOnly.FromDateTime(date);

                if (nonWorkingDays.Contains(dateOnly))
                {
                    continue;
                }

                workingDays++;
            }

            return workingDays;
        }
    }
}
