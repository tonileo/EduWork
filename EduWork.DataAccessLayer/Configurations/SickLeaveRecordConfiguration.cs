using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer.Entites.Abstractions;

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

            builder
                .Property(x => x.StartDate)
                .IsRequired();

            builder
                .Property(x => x.EndDate)
                .IsRequired();

            builder
                .Property(x => x.Comment)
                .HasMaxLength(EntityConstants.LONG_LENGTH_TEXT);
        }
    }
}
