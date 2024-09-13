namespace Common.DTO.Profile
{
    public record UserProfileDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public CurrentUserProjectDto? Project { get; set; }
    }
}
