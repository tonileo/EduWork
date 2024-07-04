using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectTimeResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ProjectTimeDto ProjectTimeDto { get; set; }
        public bool ProjectIsPayable { get; set; }
    }
}
