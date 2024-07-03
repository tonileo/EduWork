using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class AnnualLeaveRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        [StringLength(200)]
        public string? Comment { get; set; }



        [Required]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; } = new();
    }
}
