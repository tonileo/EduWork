namespace Common.DTO.ProjectTime
{
    public record ProjectTimeResponseDto
    {
        public List<ProjectTimeDto>? ProjectTimes { get; set; }
        public List<ProjectTimeSumDto>? ProjectTimeSums { get; set; }

        public int SumAllProjectTimesHours { get; set; }
        public int SumAllProjectTimesMinutes { get; set; }
    }
}
