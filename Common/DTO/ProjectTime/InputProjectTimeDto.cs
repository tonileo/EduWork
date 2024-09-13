namespace Common.DTO.ProjectTime
{
    public record InputProjectTimeDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int TimeSpentMinutes { get; set; }

        public int Hours { get; set; }
        public int Minutes { get; set; }

        public DateOnly DateWorkDay { get; set; }
        public string? TitleProject { get; set; }
    }
}
