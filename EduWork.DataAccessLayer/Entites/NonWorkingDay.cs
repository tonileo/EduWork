using System.ComponentModel.DataAnnotations;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class NonWorkingDay : BaseEntity
    {
        [Required]
        public DateOnly NonWorkingDate { get; set; }

        [Required]
        [StringLength(EntityConstants.SHORT_LENGTH_TEXT)]
        public string Title { get; set; } = string.Empty;
    }
}
