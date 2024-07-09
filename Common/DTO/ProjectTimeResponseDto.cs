using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectTimeResponseDto
    {
        public List<ProjectTimeDto> ProjectTimes { get; set; }
        public List<ProjectTimeSumDto> ProjectTimeSums { get; set; }

        public int SumAllProjectTimes { get; set; }
    }
}
