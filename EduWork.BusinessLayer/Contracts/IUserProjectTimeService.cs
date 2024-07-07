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
        public Task InputProjectTime(ProjectTimeRequestDto projectTime);
        public Task<List<ProjectTimeDtoTest>> GetProjectTimes();
        public Task<List<ProjectTimeDto>> GetProjectTimesFilter(string? fromDate, string? toDate, string username, string projectTitle);
        public Task<List<ProjectTimeDto>> GetMyProjectTimesFilter(string email, string? fromDate, string? toDate, string projectTitle);
    }
}
