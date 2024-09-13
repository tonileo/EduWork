namespace Common.DTO.ProjectTime
{
    public record InputProjectTimeResponseDto
    {
        public List<InputProjectTimeDto>? InputProjectTimeDto { get; set; }

        public int SumTimeSpentHours { get; set; }
        public int SumTimeSpentMinutes { get; set; }
    }
}
