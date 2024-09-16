using System.Reflection;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace EduWork.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<NonWorkingDay> NonWorkingDays { get; set; }
        public DbSet<AnnualLeave> AnnualLeaves { get; set; }
        public DbSet<AnnualLeaveRecord> AnnualLeavesRecords { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<ProjectTime> ProjectTimes { get; set; }
        public DbSet<SickLeaveRecord> SickLeaveRecords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProjectRole> UserProjectRoles { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
