namespace Common.DTO.ProjectTime
{
    public record ProjectSmallDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? LastChosenTitle { get; set; }
    }
}
