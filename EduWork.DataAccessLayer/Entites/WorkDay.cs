using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class WorkDay
    {
        [Key]
        public int Id { get; set; }
        public DateOnly WorkDate { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
