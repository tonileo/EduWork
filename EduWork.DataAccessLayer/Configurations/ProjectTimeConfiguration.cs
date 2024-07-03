using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class ProjectTimeConfiguration : IEntityTypeConfiguration<ProjectTime>
    {
        public void Configure(EntityTypeBuilder<ProjectTime> builder)
        {
            builder
                .HasOne(b => b.WorkDay)
                .WithMany(s => s.ProjectTimes)
                .HasForeignKey(c => c.WorkDayId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(b => b.Project)
                .WithMany(s => s.ProjectTimes)
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
