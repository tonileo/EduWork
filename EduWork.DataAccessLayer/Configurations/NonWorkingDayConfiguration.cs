using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class NonWorkingDayConfiguration : IEntityTypeConfiguration<NonWorkingDay>
    {
        public void Configure(EntityTypeBuilder<NonWorkingDay> builder)
        {
            builder
                .HasIndex(x => x.NonWorkingDate)
                .IsUnique();

            builder.Property(x => x.Title)
                .HasMaxLength(EntityConstants.SHORT_LENGTH_TEXT)
                .IsRequired();
        }
    }
}
