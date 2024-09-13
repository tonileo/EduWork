using System.ComponentModel.DataAnnotations;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class SickLeaveRecord : BaseEntity
    {
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        [Required]
        [StringLength(EntityConstants.LONG_LENGTH_TEXT)]
        public string? Comment { get; set; }


        [Required]
        public int UserId { get; set; }
        [Required]
        public virtual User? User { get; set; }
    }
}
