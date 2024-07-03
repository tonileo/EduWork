using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class AnnualLeave : BaseEntity
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public int TotalLeaveDays { get; set; }
        [Required]
        public int LeftLeaveDays { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
