using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites;
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
        }
    }
}
