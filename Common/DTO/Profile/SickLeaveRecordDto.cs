namespace Common.DTO.Profile
{
    public record SickLeaveRecordDto
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TotalSickDays { get; set; }
        public string? Comment { get; set; }
    }
}
