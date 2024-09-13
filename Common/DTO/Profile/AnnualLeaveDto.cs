namespace Common.DTO.Profile
{
    public record AnnualLeaveDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int TotalLeaveDays { get; set; }
        public int LeftLeaveDays { get; set; }

        public int LeftLeaveDaysLastYear { get; set; }
        public int PlannedLeaveDays { get; set; }
    }
}
