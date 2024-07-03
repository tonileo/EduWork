using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class UserProjectRole
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int ProjectRoleId { get; set; }

        [Required]
        public virtual User User { get; set; } = new();

        [Required]
        public virtual Project Project { get; set; } = new();

        [Required]
        public virtual ProjectRole ProjectRole { get; set; } = new();
    }
}
