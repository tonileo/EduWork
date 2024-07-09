using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using EduWork.DataAccessLayer.Entites;

namespace EduWork.BusinessLayer.Contracts
{
    public interface IUserProjectTimeService
    {
        public Task InputProjectTime(string email, ProjectTimeRequestDto projectTime);
        public Task UpdateProjectTime(string email, int id, ProjectTimeUpdateRequestDto projectTime);
        public Task<List<ProjectTimeDtoTest>> GetProjectTimes();
        public Task<ProjectTimeResponseDto> GetProjectTimesFilter(DateTime? fromDate, DateTime? toDate, string? username, string? projectTitle);
        public Task<ProjectTimeResponseDto> GetMyProjectTimesFilter(string email, DateTime? fromDate, DateTime? toDate, string? projectTitle);
    }
}
