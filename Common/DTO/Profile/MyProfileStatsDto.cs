namespace Common.DTO.Profile
{
    public record MyProfileStatsDto
    {
        public int SumHoursSpent { get; set; }
        public int SumMinutesSpent { get; set; }
        public int CountProjects { get; set; }
    }
}
