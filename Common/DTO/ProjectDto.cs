using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
        public bool IsPrivate { get; set; }       
        public bool IsEducation { get; set; }
        public bool IsPayable { get; set; }
        public string DevOpsProjectId { get; set; }
    }
}
