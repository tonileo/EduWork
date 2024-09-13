using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class AnnualLeaveRecordConfiguration : IEntityTypeConfiguration<AnnualLeaveRecord>
    {
        public void Configure(EntityTypeBuilder<AnnualLeaveRecord> builder)
        {
            builder
                .HasOne(b => b.User)
                .WithMany(s => s.AnnualLeaveRecords)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
