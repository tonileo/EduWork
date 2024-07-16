using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.ProjectTime
{
    public record ProjectTimeRequestDto
    {
        [Required(ErrorMessage = "Vrijeme provedeno na projektu je obavezno.")]
        [Range(1, 480, ErrorMessage = "Vrijeme mora biti između 1 i 480 minuta.")]
        public int TimeSpentMinutes { get; set; }

        [Required(ErrorMessage = "Odabir projekta je obavezan.")]
        public string? TitleProject { get; set; }

        [Required]
        [DateNotInFuture(ErrorMessage = "Radni dan ne može biti u budućnosti.")]
        public DateTime DateWorkDay { get; set; }

        public string? Comment { get; set; }
    }
}
