using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.CustomValidations;

namespace Common.DTO.ProjectTime
{
    public record FilterModel
    {
        [DateNotInFuture(ErrorMessage = "Workday cannot be in the future")]
        public DateTime? FromDate { get; set; } = DateTime.Today.AddMonths(-1);

        [DateNotInFuture(ErrorMessage = "Workday cannot be in the future")]
        public DateTime? ToDate { get; set; } = DateTime.Today;

        public string? Username { get; set; }

        [StringLength(100, ErrorMessage = "Project title cannot be longer than 100 characters")]
        public string? ProjectTitle { get; set; }
    }
}
