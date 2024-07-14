using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectTimeSumDto
    {
        public string? TitleProject { get; set; }
        public int SumTimeSpentHours { get; set; }
        public int SumTimeSpentMinutes { get; set; }

        public int PercentageTimeSpent {  get; set; }

        public bool IsFinished { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsEducation { get; set; }
        public bool IsPayable { get; set; }
    }
}
