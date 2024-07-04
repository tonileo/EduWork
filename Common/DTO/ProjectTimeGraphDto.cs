using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectTimeGraphDto
    {
        public int Id { get; set; }
        public int TimeSpentMinutes { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
