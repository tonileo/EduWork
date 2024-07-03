using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public virtual User User { get; set; }
    }
}
