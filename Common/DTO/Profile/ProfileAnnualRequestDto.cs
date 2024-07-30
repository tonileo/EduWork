using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Profile
{
    public record ProfileAnnualRequestDto
    {
        public DateTime StartDate {  get; set; }
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
    }
}
