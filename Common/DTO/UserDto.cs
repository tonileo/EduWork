using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string EntraObjectId { get; set; }
        //public int AppRoleId { get; set; }
        //public AppRoleDto AppRoleDto { get; set; }

        //public List<AnnualLeaveDto> AnnualLeavesDto { get; set; }
        //public List<WorkDayDto> WorkDaysDto { get; set; }
        //public List<UserProjectRoleDto> UserProjectRolesDto { get; set; }
        //public List<SickLeaveRecordDto> SickLeaveRecordsDto { get; set; }
        //public List<AnnualLeaveRecordDto> AnnualLeaveRecordsDto { get; set; }
    }
}
