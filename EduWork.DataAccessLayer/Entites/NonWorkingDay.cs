using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
