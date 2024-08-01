using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.ProjectTime
{
    public record OvertimeDto
    {
        public int OvertimeHours {  get; set; }
        public int OvertimeMinutes { get; set; }
    }
}
