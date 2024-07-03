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
    internal class AnnualLeaveConfiguration : IEntityTypeConfiguration<AnnualLeave>
    {
        public void Configure(EntityTypeBuilder<AnnualLeave> builder)
        {
            builder
                .HasIndex(x => new { x.UserId, x.Year })
                .IsUnique();

            builder
                .HasOne(b => b.User)
                .WithMany(s => s.AnnualLeaves)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
