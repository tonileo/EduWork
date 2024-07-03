using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;

namespace EduWork.BusinessLayer.Contracts
{
    public interface IUserProfileService
    {
        public Task<List<UserProfileDto>> GetAllUserProfiles();
        public Task<UserProfileDto> GetUserProfile(int id);
        public Task<List<SickLeaveRecordDto>> GetUserSickLeaveRecords(int id);
        public Task<List<AnnualLeaveDto>> GetUserAnnualLeave(int id);
    }
}
