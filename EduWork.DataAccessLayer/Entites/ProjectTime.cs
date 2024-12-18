using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class ProjectTime : BaseEntity
    {
        public string? Comment { get; set; }
        public required int TimeSpentMinutes { get; set; }

        public int WorkDayId { get; set; }
        public int ProjectId { get; set; }
        public virtual WorkDay? WorkDay { get; set; }
        public virtual Project? Project { get; set; }
    }
}
