using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.Profile;

namespace EduWork.BusinessLayer.Contracts
{
    public interface IUserProfileService
    {
        public Task<CurrentUserProjectDto> GetUserCurrentProject(int id);
        public Task<List<ProjectsProfileDto>> GetProjects();
        public Task<List<UserProfileDto>> GetUserSmallProfiles(string? username, bool? asc);
        public Task<List<SickLeaveRecordDto>> GetUserSickLeaveRecords(int id);
        public Task<AnnualLeaveDto> GetUserAnnualLeaves(int id);
        public Task<List<AnnualLeaveRecordDto>> GetUserAnnualLeaveRecords(int id);
        public Task<MyProfileDto> GetUserProfile(string? email);
        public Task<MyProfileStatsDto> GetMyProfileStats(string? email, bool thisMonth, bool lastMonth);
        public Task AddAnnualLeave(string? username, ProfileAnnualRequestDto annualLeave);
        public Task AddSickDay(string? username, ProfileAnnualRequestDto annualLeave);
    }
}
