using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class WorkDayTime
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public int WorkDayId { get; set; }

        [Required]
        public virtual WorkDay WorkDay { get; set; } = new();
    }
}
