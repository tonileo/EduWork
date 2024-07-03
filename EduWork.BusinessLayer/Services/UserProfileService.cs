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

namespace EduWork.BusinessLayer.Services
{
    public class UserProfileService(AppDbContext context) : IUserProfileService
    {
        public async Task<List<UserProfileDto>> GetAllUserProfiles()
        {
            var users = await context.Users.ToListAsync();

            var userProfiles = new List<UserProfileDto>();
            foreach (var user in users)
            {
                userProfiles.Add(new UserProfileDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                });
            }
            return userProfiles;
        }

        public async Task<UserProfileDto> GetUserProfile(int id)
        {
            var user = await context.Users.FindAsync(id);

            var userProfile = new UserProfileDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return userProfile;
        }

        public async Task<List<SickLeaveRecordDto>> GetUserSickLeaveRecords(int id)
        {
            var sickLeaveRecord = await context.SickLeaveRecords.Where(s => s.UserId == id).ToListAsync();
            var userSickLeave = new List<SickLeaveRecordDto>();

            foreach (var record in sickLeaveRecord)
            {
                userSickLeave.Add(new SickLeaveRecordDto()
                {
                    Id = record.Id,
                    StartDate = record.StartDate,
                    EndDate = record.EndDate,
                    Comment = record.Comment
                });
            }

            return userSickLeave;
        }

        public async Task<List<AnnualLeaveDto>> GetUserAnnualLeave(int id)
        {
            var annualLeave = await context.AnnualLeaves.Where(s => s.UserId == id).ToListAsync();
            var userAnnualLeave = new List<AnnualLeaveDto>();

            foreach (var record in annualLeave)
            {
                userAnnualLeave.Add(new AnnualLeaveDto()
                {
                    Id = record.Id,
                    Year = record.Year,
                    TotalLeaveDays = record.TotalLeaveDays,
                    LeftLeaveDays = record.LeftLeaveDays
                });
            }

            return userAnnualLeave;
        }
    }
}
