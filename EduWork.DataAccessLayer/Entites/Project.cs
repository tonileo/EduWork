using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.DataAccessLayer.Entites
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public bool IsFinished { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        [Required]
        public bool IsEducation { get; set; }
        [Required]
        public bool IsPayable { get; set; }
        [Required]
        [StringLength(200)]
        public string DevOpsProjectId { get; set; }
    }
}
