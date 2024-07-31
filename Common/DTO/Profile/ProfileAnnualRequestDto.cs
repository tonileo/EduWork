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
        [Required(ErrorMessage = "Potrebno popuniti početni datum")]
        public DateTime StartDate {  get; set; }

        [Required(ErrorMessage = "Potrebno popuniti završni datum")]
        public DateTime EndDate { get; set; }

        public string? Comment { get; set; }
    }
}
