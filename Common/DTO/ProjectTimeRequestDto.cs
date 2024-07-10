using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectTimeRequestDto
    {
        public string? Comment { get; set; }
        public int TimeSpentMinutes { get; set; }

        public string? TitleProject { get; set; }

        public DateTime DateWorkDay { get; set; } = DateTime.Today;
    }
}
