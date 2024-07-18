using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.ProjectTime
{
    public record ProjectTimeHistoryDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int TimeSpentMinutes { get; set; }


        public DateOnly DateWorkDay { get; set; }
        public string? TitleProject { get; set; }


        public int SumTimeSpentHours { get; set; }
        public int SumTimeSpentMinutes { get; set; }
        public bool IsNonWorkingDay { get; set; }
    }
}
