using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string EntraObjectId { get; set; }


        [Required]
        public int AppRoleId { get; set; }
        [Required]
        public virtual AppRole AppRole { get; set; }


        [Required]
        public virtual ICollection<AnnualLeave> AnnualLeaves { get; set; }
        [Required]
        public virtual ICollection<WorkDay> WorkDays { get; set; }
        [Required]
        public virtual ICollection<UserProjectRole> UserProjectRoles { get; set; }
        [Required]
        public virtual ICollection<SickLeaveRecord> SickLeaveRecords { get; set; }
        [Required]
        public virtual ICollection<AnnualLeaveRecord> AnnualLeaveRecords { get; set;}
    }
}
