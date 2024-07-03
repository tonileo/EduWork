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
    internal class NonWorkingDayConfiguration : IEntityTypeConfiguration<NonWorkingDay>
    {
        public void Configure(EntityTypeBuilder<NonWorkingDay> builder)
        {
            builder
                .HasIndex(x => x.NonWorkingDate)
                .IsUnique();
        }
    }
}
