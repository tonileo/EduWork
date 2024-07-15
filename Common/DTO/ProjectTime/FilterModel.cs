using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.ProjectTime
{
    public record FilterModel
    {
        [DateNotInFuture(ErrorMessage = "Radni dan ne može biti u budućnosti.")]
        public DateTime? FromDate { get; set; } = DateTime.Today.AddMonths(-1).ToUniversalTime();

        [DateNotInFuture(ErrorMessage = "Radni dan ne može biti u budućnosti.")]
        public DateTime? ToDate { get; set; } = DateTime.Today.ToUniversalTime();

        public string? Username { get; set; }

        [StringLength(100, ErrorMessage = "Project title cannot be longer than 100 characters.")]
        public string? ProjectTitle { get; set; }
    }
}
