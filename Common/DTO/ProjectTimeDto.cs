using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectTimeDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int TimeSpentMinutes { get; set; }
    }
}
