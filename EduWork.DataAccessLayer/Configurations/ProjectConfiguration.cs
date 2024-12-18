using EduWork.DataAccessLayer.Entites;
using EduWork.DataAccessLayer.Entites.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasIndex(x => x.Title)
                .IsUnique();

            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(EntityConstants.SHORT_LENGTH_TEXT);

            builder
                .Property(x => x.StartDate)
                .IsRequired();

            builder
                .Property(x => x.EndDate)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(EntityConstants.LONG_LENGTH_TEXT);

            builder
                .Property(x => x.IsFinished)
                .IsRequired();

            builder
                .Property(x => x.IsPrivate)
                .IsRequired();

            builder
                .Property(x => x.IsEducation)
                .IsRequired();

            builder
                .Property(x => x.IsPayable)
                .IsRequired();

            builder
                .Property(x => x.DevOpsProjectId)
                .IsRequired()
                .HasMaxLength(EntityConstants.LONG_LENGTH_TEXT);
        }
    }
}
