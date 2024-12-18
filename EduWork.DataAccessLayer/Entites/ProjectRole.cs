using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class ProjectRole : BaseEntity
    {
        public required string Title { get; set; } = string.Empty;
        public string? Description { get; set; }


        public virtual ICollection<UserProjectRole> UserProjectRoles { get; set; } = [];
    }
}
