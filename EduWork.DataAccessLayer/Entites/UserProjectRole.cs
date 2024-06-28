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


        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectRoleId { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }
}
