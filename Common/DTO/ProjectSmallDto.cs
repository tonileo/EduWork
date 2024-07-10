using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectSmallDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsFinished { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsEducation { get; set; }
        public bool IsPayable { get; set; }
    }
}
