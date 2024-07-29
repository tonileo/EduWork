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
        public Task<CurrentUserProjectDto> GetUserCurrentProject(string? email);
        public Task<List<UserProfileDto>> GetUsers();
        public Task<UserProfileDto> GetUser(int id);
        public Task<List<SickLeaveRecordDto>> GetUserSickLeaveRecords(int id);
        public Task<List<AnnualLeaveDto>> GetUserAnnualLeaves(int id);
        public Task<List<AnnualLeaveRecordDto>> GetUserAnnualLeaveRecords(int id);
    }
}
