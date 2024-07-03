using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class WorkDayTime : BaseEntity
    {
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
