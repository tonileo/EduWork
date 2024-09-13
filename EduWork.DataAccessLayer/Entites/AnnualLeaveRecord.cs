using System.ComponentModel.DataAnnotations;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class AnnualLeaveRecord : BaseEntity
    {
        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [StringLength(200)]
        public string? Comment { get; set; }



        [Required]
        public int UserId { get; set; }

        [Required]
        public virtual User? User { get; set; }
    }
}
