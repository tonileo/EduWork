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
        public async Task<CurrentUserProjectDto> GetUserCurrentProject(string? email)
        {
            var userProjectTime = await context.ProjectTimes
                .Include(s => s.Project)
                .Include(a => a.WorkDay)
                .Where(g => g.WorkDay.User.Email == email)
                .OrderByDescending(pt => pt.WorkDay.WorkDate)
                .Select(m => m.Project.Title)
                .FirstOrDefaultAsync();

            var userProjectTimeRole = await context.UserProjectRoles
                .Include(s => s.User)
                .Include(u => u.ProjectRole)
                .Where(g => g.User.Email == email)
                .Where(b => b.Project.Title == userProjectTime)
                .Select (m => m.ProjectRole.Title)
                .FirstOrDefaultAsync();

            var userProjects = new CurrentUserProjectDto
            {
                Title = userProjectTime,
                Role = userProjectTimeRole
            };

            return userProjects;
        }

        public async Task<List<UserProfileDto>> GetUsers()
        {
            var users = await context.Users.ToListAsync();

            var userProfiles = mapper.Map<List<UserProfileDto>>(users);

            return userProfiles;
        }

        public async Task<UserProfileDto> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);

            var userProfile = mapper.Map<UserProfileDto>(user);

            return userProfile;
        }

        public async Task<List<SickLeaveRecordDto>> GetUserSickLeaveRecords(int id)
        {
            var sickLeaveRecord = await context.SickLeaveRecords.Where(s => s.UserId == id).ToListAsync();

            var userSickLeave = new List<SickLeaveRecordDto>();

            foreach (var record in sickLeaveRecord)
            {
                var days = new List<DateOnly>();

                for (var date = record.StartDate; date <= record.EndDate; date = date.AddDays(1))
                {
                    days.Add(date);
                }

                int totalSickDays = days.Count;

                var sickLeaveDto = new SickLeaveRecordDto
                {
                    Id = record.Id,
                    SickDays = days,
                    TotalSickDays = totalSickDays,
                    Comment = record.Comment
                };

                userSickLeave.Add(sickLeaveDto);
            }

            return userSickLeave;
        }

        public async Task<List<AnnualLeaveDto>> GetUserAnnualLeaves(int id)
        {
            var annualLeave = await context.AnnualLeaves.Where(s => s.UserId == id).ToListAsync();

            var userAnnualLeave = mapper.Map<List<AnnualLeaveDto>>(annualLeave);

            return userAnnualLeave;
        }

        public async Task<List<AnnualLeaveRecordDto>> GetUserAnnualLeaveRecords(int id)
        {
            var annualLeave = await context.AnnualLeavesRecords.Where(s => s.UserId == id).ToListAsync();

            var userAnnualLeave = mapper.Map<List<AnnualLeaveRecordDto>>(annualLeave);

            return userAnnualLeave;
        }

        public async Task<MyProfileDto> GetUserProfile(string? email)
        {
            var user = await context.Users.Where(u => u.Email == email).SingleOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("User doesn't exist");
            }

            var projects = await GetUserCurrentProject(email);
            var sickLeaveRecords = await GetUserSickLeaveRecords(user.Id);
            var annualLeaves = await GetUserAnnualLeaves(user.Id);
            var annualLeave = from al in annualLeaves where al.Year == DateTime.Now.Year select al;
            var annualLeaveRecords = await GetUserAnnualLeaveRecords(user.Id);

            var userProfile = new MyProfileDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Project = projects,
                AnnualLeave = annualLeave.FirstOrDefault(),
                AnnualLeaveRecords = annualLeaveRecords,
                SickLeaveRecords = sickLeaveRecords
            };

            return userProfile;
        }

        public async Task<MyProfileStatsDto> GetMyProfileStats(string? email, bool thisMonth, bool lastMonth)
        {
            IQueryable<ProjectTime> query = context.ProjectTimes
                .Include(k => k.Project)
                .Where(pt => pt.WorkDay.User.Email == email)
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
    }
}
