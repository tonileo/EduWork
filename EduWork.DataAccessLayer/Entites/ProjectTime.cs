using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class ProjectTime
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Comment { get; set; }
        [Required]
        public int TimeSpentMinutes { get; set; }

        public int WorkDayId { get; set; }
        public int ProjectId { get; set; }
        public WorkDay WorkDay { get; set; }
        public Project Project { get; set; }
    }
}
