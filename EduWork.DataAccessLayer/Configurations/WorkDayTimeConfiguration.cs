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
    internal class WorkDayTimeConfiguration : IEntityTypeConfiguration<WorkDayTime>
    {
        public void Configure(EntityTypeBuilder<WorkDayTime> builder)
        {
            builder
                .HasOne(p => p.WorkDay)
                .WithMany(w => w.WorkDayTimes)
                .HasForeignKey(ps => ps.WorkDayId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
