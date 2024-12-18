using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class UserProjectRole : BaseEntity
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectRoleId { get; set; }

        public virtual User? User { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ProjectRole? ProjectRole { get; set; }
    }
}
