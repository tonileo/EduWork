using System.ComponentModel.DataAnnotations;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class AppRole : BaseEntity
    {
        [Required]
        [StringLength(EntityConstants.SHORT_LENGTH_TEXT)]
        public string Title { get; set; } = string.Empty;

        [StringLength(EntityConstants.LONG_LENGTH_TEXT)]
        public string? Description { get; set; }


        public virtual ICollection<User> Users { get; set; } = [];
    }
}
