using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EduWork.DataAccessLayer.Configurations
{
    internal class UserProjectRoleConfiguration : IEntityTypeConfiguration<UserProjectRole>
    {
        public void Configure(EntityTypeBuilder<UserProjectRole> builder)
        {
            builder
                .HasOne(p => p.User)
                .WithMany(w => w.UserProjectRoles)
                .HasForeignKey(ps => ps.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.Project)
                .WithMany(b => b.UserProjectRoles)
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(d => d.ProjectRole)
                .WithMany(e => e.UserProjectRoles)
                .HasForeignKey(f => f.ProjectRoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
