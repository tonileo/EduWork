using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class NonWorkingDay
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateOnly NonWorkingDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
    }
}
