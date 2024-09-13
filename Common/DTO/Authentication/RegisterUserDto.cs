using System.ComponentModel.DataAnnotations;

namespace Common.DTO.Authentication
{
    public record RegisterUserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string? Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; } = string.Empty;


        [Required]
        public int AppRoleId { get; set; }
    }
}
