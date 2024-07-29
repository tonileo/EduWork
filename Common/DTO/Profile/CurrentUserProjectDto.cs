using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Profile
{
    public record CurrentUserProjectDto
    {
        public string? Title { get; set; }
        public string? Role { get; set; }
    }
}
