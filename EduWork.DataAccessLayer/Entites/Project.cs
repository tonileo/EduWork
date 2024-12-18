using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class Project : BaseEntity
    {
        public required string Title { get; set; } = string.Empty;

        public required DateOnly StartDate { get; set; }

        public required DateOnly EndDate { get; set; }

        public string? Description { get; set; } = string.Empty;

        public required bool IsFinished { get; set; } = false;

        public required bool IsPrivate { get; set; }

        public required bool IsEducation { get; set; }

        public required bool IsPayable { get; set; }

        public required string DevOpsProjectId { get; set; } = string.Empty;


        public virtual ICollection<UserProjectRole> UserProjectRoles { get; set; } = [];
        public virtual ICollection<ProjectTime> ProjectTimes { get; set; } = [];
    }
}
