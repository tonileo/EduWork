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
        public Task<MyProfileDto> GetUserProfile(string? username);
        public Task<MyProfileStatsDto> GetMyProfileStats(string? username, bool thisMonth, bool lastMonth);
        public Task AddAnnualLeave(string? username, ProfileAnnualRequestDto annualLeave);
        public Task AddSickDay(string? username, ProfileAnnualRequestDto annualLeave);
    }
}
