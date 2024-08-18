using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.CustomValidations;

namespace Common.DTO.ProjectTime
{
    public record ProjectTimeRequestDto
    {
        [Required(ErrorMessage = "Time spent on the project is required")]
        [Range(1, 480, ErrorMessage = "Time must be between 1 and 480 minutes")]
        public int TimeSpentMinutes { get; set; }

        [Required(ErrorMessage = "Selecting a project is mandatory")]
        public string? TitleProject { get; set; }

        [Required]
        [DateNotInFuture(ErrorMessage = "Workday cannot be in the future")]
        [DateNotWeekend(ErrorMessage = "Workday cannot be a weekend")]
        public DateTime DateWorkDay { get; set; }

        public string? Comment { get; set; }
    }
}
