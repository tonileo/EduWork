namespace Common.DTO.ProjectTime
{
    public record ProjectTimeDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int TimeSpentMinutes { get; set; }

        public string? TitleProject { get; set; }
    }
}
