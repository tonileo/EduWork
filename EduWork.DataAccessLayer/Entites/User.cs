using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(EntityConstants.SHORT_LENGTH_TEXT)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(EntityConstants.SHORT_LENGTH_TEXT)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; } = string.Empty;

        [StringLength(EntityConstants.LONG_LENGTH_TEXT)]
        public string EntraObjectId { get; set; } = string.Empty;


        [Required]
        public int AppRoleId { get; set; }

        [Required]
        public virtual AppRole? AppRole { get; set; }


        public virtual ICollection<AnnualLeave> AnnualLeaves { get; set; } = [];
        public virtual ICollection<WorkDay> WorkDays { get; set; } = [];
        public virtual ICollection<UserProjectRole> UserProjectRoles { get; set; } = [];
        public virtual ICollection<SickLeaveRecord> SickLeaveRecords { get; set; } = [];
        public virtual ICollection<AnnualLeaveRecord> AnnualLeaveRecords { get; set; } = [];
    }
}
