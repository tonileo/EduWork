using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class ProjectTime
    {
        [Key]
        public int Id { get; set; }

        [StringLength(EntityConstants.LONG_LENGTH_TEXT)]
        public string? Comment { get; set; }

        [Required]
        public int TimeSpentMinutes { get; set; }


        [Required]
        public int WorkDayId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public virtual WorkDay WorkDay { get; set; } = new();

        [Required]
        public virtual Project Project { get; set; } = new();
    }
}
