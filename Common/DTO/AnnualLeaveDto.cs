using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record AnnualLeaveDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int TotalLeaveDays { get; set; }
        public int LeftLeaveDays { get; set; }
    }
}
