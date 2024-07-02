using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class WorkDay
    {
        [Key]
        public int Id { get; set; }
        public DateOnly WorkDate { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public virtual User User { get; set; }


        public virtual ICollection<WorkDayTime> WorkDayTimes { get; set; }
        public virtual ICollection<ProjectTime> ProjectTimes { get; set; }
    }
}
