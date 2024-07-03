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
    internal class SickLeaveRecordConfiguration : IEntityTypeConfiguration<SickLeaveRecord>
    {
        public void Configure(EntityTypeBuilder<SickLeaveRecord> builder)
        {
            builder
                .HasOne(b => b.User)
                .WithMany(s => s.SickLeaveRecords)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
