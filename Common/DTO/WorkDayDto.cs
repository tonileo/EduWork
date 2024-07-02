using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record WorkDayDto
    {
        public int Id { get; set; }
        public DateOnly WorkDate { get; set; }
        public int UserId { get; set; }
    }
}
