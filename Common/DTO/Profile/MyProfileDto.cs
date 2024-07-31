using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Profile
{
    public record MyProfileDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? AppRoleTitle { get; set; }
        public string? Email { get; set; }
        public CurrentUserProjectDto? Project { get; set; }
        public AnnualLeaveDto? AnnualLeave { get; set; }
        public List<AnnualLeaveRecordDto>? AnnualLeaveRecords { get; set; }
        public List<SickLeaveRecordDto>? SickLeaveRecords { get; set; }
    }
}
