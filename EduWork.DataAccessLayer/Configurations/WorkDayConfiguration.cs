using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class WorkDayConfiguration : IEntityTypeConfiguration<WorkDay>
    {
        public void Configure(EntityTypeBuilder<WorkDay> builder)
        {
            builder
                .HasIndex(x => new { x.UserId, x.WorkDate})
                .IsUnique();

            builder
                .HasOne(u => u.User)
                .WithMany(w => w.WorkDays)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.WorkDate)
                .IsRequired();
        }
    }
}
