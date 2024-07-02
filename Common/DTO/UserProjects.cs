using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record UserProjects
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
