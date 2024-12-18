using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class AppRole : BaseEntity
    {
        public required string Title { get; set; } = string.Empty;

        public string? Description { get; set; }


        public virtual ICollection<User> Users { get; set; } = [];
    }
}
