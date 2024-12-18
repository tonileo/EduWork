using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer.Entites.Abstractions;

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

            builder
                .Property(x => x.Comment)
                .HasMaxLength(EntityConstants.LONG_LENGTH_TEXT);

            builder
                .Property(x => x.TimeSpentMinutes)
                .IsRequired();
        }
    }
}
