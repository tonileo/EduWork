namespace Common.DTO.Profile
{
    public record AnnualLeaveRecordDto
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Comment { get; set; }
    }
}
