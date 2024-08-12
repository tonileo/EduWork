using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.ProjectTime;
using EduWork.DataAccessLayer.Entites;

namespace EduWork.BusinessLayer.Contracts
{
    public interface IUserProjectTimeService
    {
        public Task<List<ProjectSmallDto>> GetProjects(string? email);
        public Task<List<UsernamesDto>> GetUsernames();
        public Task<InputProjectTimeResponseDto> GetMyInputProjectTimes(string? email, DateTime userWorkDay);
        public Task<OvertimeDto> GetOverTime(string? email);
        public Task<List<InputProjectTimeDto>> GetMyProjectTimes(string? email, DateTime userWorkDay);
        public Task<List<InputProjectTimeDto>> GetProjectTimes(string? username, DateTime userWorkDay);
        public Task InputProjectTime(string email, ProjectTimeRequestDto projectTime);
        public Task UpdateProjectTime(string email, int id, ProjectTimeRequestDto projectTime);
        public Task DeleteProjectTime (int id);
        public Task<ProjectTimeResponseDto> GetProjectTimesFilter(DateTime? fromDate, DateTime? toDate, string? username, string? projectTitle);
        public Task<ProjectTimeResponseDto> GetMyProjectTimesFilter(string? email, DateTime? fromDate, DateTime? toDate, string? projectTitle);
        public Task<List<ProjectTimeHistoryDto>> GetMyHistoryProjectTimesFilter(string? email, bool? thisMonth, bool? lastMonth, bool? thisQuarter);
        public Task<List<ProjectTimeHistoryDto>> GetHistoryProjectTimesFilter(bool? thisMonth, bool? lastMonth, bool? thisQuarter, string? username);
    }
}
