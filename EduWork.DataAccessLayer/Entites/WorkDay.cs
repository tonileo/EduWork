using System.ComponentModel.DataAnnotations;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class WorkDay : BaseEntity
    {
        [Required]
        public DateOnly WorkDate { get; set; }


        [Required]
        public int UserId { get; set; }

        [Required]
        public virtual User? User { get; set; }


        public virtual ICollection<ProjectTime> ProjectTimes { get; set; } = [];
    }
}
