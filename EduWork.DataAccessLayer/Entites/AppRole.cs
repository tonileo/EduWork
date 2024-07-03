using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class AppRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(EntityConstants.SHORT_LENGTH_TEXT)]
        public string Title { get; set; } = string.Empty;

        [StringLength(EntityConstants.LONG_LENGTH_TEXT)]
        public string? Description { get; set; }


        public virtual ICollection<User> Users { get; set; } = [];
    }
}
