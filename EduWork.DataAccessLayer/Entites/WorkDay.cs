using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class WorkDay : BaseEntity
    {
        public required DateOnly WorkDate { get; set; }
        public int UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<ProjectTime> ProjectTimes { get; set; } = [];
    }
}
