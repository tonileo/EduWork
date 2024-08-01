using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.ProjectTime
{
    public record InputProjectTimeResponseDto
    {
        public List<InputProjectTimeDto>? InputProjectTimeDto { get; set; }

        public int SumTimeSpentHours { get; set; }
        public int SumTimeSpentMinutes { get; set; }
    }
}
