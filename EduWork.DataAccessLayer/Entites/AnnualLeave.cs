using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class AnnualLeave
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateOnly Year { get; set; } //UPITNO
        [Required]
        public int TotalLeaveDays { get; set; }
        [Required]
        public int LeftLeaveDays { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
