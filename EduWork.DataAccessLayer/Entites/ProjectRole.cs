using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class ProjectRole
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Description { get; set; }


        [Required]
        public virtual ICollection<UserProjectRole> UserProjectRoles { get; set; }
    }
}
