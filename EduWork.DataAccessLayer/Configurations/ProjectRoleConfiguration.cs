using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class ProjectRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
    {
        public void Configure(EntityTypeBuilder<ProjectRole> builder)
        {
            builder
                .HasIndex(x =>  x.Id)
                .IsUnique();

            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(EntityConstants.SHORT_LENGTH_TEXT);

            builder
                .Property(x => x.Description)
                .HasMaxLength(EntityConstants.LONG_LENGTH_TEXT);
        }
    }
}
