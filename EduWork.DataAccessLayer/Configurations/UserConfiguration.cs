using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .HasIndex(y => y.Username)
                .IsUnique();

            builder
                .HasOne(b => b.AppRole)
                .WithMany(s => s.Users)
                .HasForeignKey(c => c.AppRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(EntityConstants.SHORT_LENGTH_TEXT);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(EntityConstants.SHORT_LENGTH_TEXT);

            builder
                .Property(x => x.Password)
                .IsRequired();

            builder
                .Property(x => x.EntraObjectId)
                .HasMaxLength(EntityConstants.LONG_LENGTH_TEXT);
        }
    }
}
