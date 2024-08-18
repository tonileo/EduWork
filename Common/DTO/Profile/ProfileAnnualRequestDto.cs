using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Profile
{
    public record ProfileAnnualRequestDto
    {
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate {  get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        public string? Comment { get; set; }
    }
}
