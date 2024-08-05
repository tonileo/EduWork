using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public  record RegisterUserDto
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
