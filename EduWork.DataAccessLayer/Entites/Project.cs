using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class Project : BaseEntity
    {
        [Required]
        [StringLength(EntityConstants.SHORT_LENGTH_TEXT)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [StringLength(EntityConstants.LONG_LENGTH_TEXT)]
        public string? Description { get; set; } = string.Empty;

        [Required]
        public bool IsFinished { get; set; }

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public bool IsEducation { get; set; }

        [Required]
        public bool IsPayable { get; set; }

        [Required]
        [StringLength(EntityConstants.LONG_LENGTH_TEXT)]
        public string DevOpsProjectId { get; set; } = string.Empty;


        public virtual ICollection<UserProjectRole> UserProjectRoles { get; set; } = [];
        public virtual ICollection<ProjectTime> ProjectTimes { get; set; } = [];
    }
}
