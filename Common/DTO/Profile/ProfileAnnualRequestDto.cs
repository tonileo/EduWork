using System.ComponentModel.DataAnnotations;

namespace Common.DTO.Profile
{
    public record ProfileAnnualRequestDto
    {
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate {  get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        public string? Comment { get; set; }
    }
}
