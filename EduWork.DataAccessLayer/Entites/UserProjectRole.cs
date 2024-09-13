using System.ComponentModel.DataAnnotations;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class UserProjectRole : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int ProjectRoleId { get; set; }

        [Required]
        public virtual User? User { get; set; }

        [Required]
        public virtual Project? Project { get; set; }

        [Required]
        public virtual ProjectRole? ProjectRole { get; set; }
    }
}
