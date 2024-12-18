using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class User : BaseEntity
    {
        public required string Username { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public string EntraObjectId { get; set; } = string.Empty;

        public int AppRoleId { get; set; }
        public virtual AppRole? AppRole { get; set; }

        public virtual ICollection<AnnualLeave> AnnualLeaves { get; set; } = [];
        public virtual ICollection<WorkDay> WorkDays { get; set; } = [];
        public virtual ICollection<UserProjectRole> UserProjectRoles { get; set; } = [];
        public virtual ICollection<SickLeaveRecord> SickLeaveRecords { get; set; } = [];
        public virtual ICollection<AnnualLeaveRecord> AnnualLeaveRecords { get; set; } = [];
    }
}
