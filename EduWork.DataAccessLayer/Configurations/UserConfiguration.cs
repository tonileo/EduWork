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
                .HasIndex(z => z.EntraObjectId)
                .IsUnique();

            builder
                .HasOne(b => b.AppRole)
                .WithMany(s => s.Users)
                .HasForeignKey(c => c.AppRoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
