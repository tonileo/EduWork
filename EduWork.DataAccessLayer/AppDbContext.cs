using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EduWork.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        

        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{

        //}

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
        public DbSet<WorkDayTime> WorkDayTimes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=EduWorkDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
