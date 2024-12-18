using EduWork.DataAccessLayer.Entites;
using EduWork.DataAccessLayer.Entites.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder
                .HasIndex(x => x.Title)
                .IsUnique();

            builder
                .HasMany(a => a.Users)
                .WithOne(b => b.AppRole)
                .HasForeignKey(c => c.AppRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Title)
                .HasMaxLength(EntityConstants.SHORT_LENGTH_TEXT)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(EntityConstants.LONG_LENGTH_TEXT);
        }
    }
}
