using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduWork.DataAccessLayer.Seed
{
    public class SeedData(AppDbContext context)
    {
        public void Run()
        {
            if (!context.NonWorkingDays.Any())
            {
                AddNonWorkingDays(context);
            }

            if (!context.ProjectRoles.Any())
            {
                AddProjectRoles(context);
            }

            if (!context.AppRoles.Any())
            {
                AddAppRoles(context);
            }

            if (!context.Users.Any())
            {
                AddUsers(context);
            }

            if (!context.Projects.Any())
            {
                AddProjects(context);
            }

            if (!context.UserProjectRoles.Any())
            {
                AddUserProjectRoles(context);
            }

            if (!context.WorkDays.Any())
            {
                AddWorkDays(context);
            }

            if (!context.ProjectTimes.Any())
            {
                AddProjectTimes(context);
            }

            if (!context.AnnualLeaves.Any())
            {
                AddAnnualLeaves(context);
            }

            if (!context.AnnualLeavesRecords.Any())
            {
                AddAnnualLeavesRecords(context);
            }

            if (!context.SickLeaveRecords.Any())
            {
                AddSickLeaveRecords(context);
            }
        }

        private static void AddNonWorkingDays(AppDbContext context)
        {

            context.NonWorkingDays.AddRange(
                new NonWorkingDay()
                {

                    NonWorkingDate = new DateOnly(2024, 1, 1),
                    Title = "New Year's Day"
                },
                new NonWorkingDay()
                {

                    NonWorkingDate = new DateOnly(2024, 12, 25),
                    Title = "Christmas Day"
                },
                new NonWorkingDay()
                {

                    NonWorkingDate = new DateOnly(2024, 7, 4),
                    Title = "Independence Day"
                },
                new NonWorkingDay()
                {

                    NonWorkingDate = new DateOnly(2024, 11, 28),
                    Title = "Thanksgiving Day"
                }
            );
            context.SaveChanges();
        }
        private static void AddProjectRoles(AppDbContext context)
        {

            context.ProjectRoles.AddRange(
                new ProjectRole()
                {

                    Title = "Developer",
                    Description = "Responsible for developing and implementing features"
                },
                new ProjectRole()
                {

                    Title = "Manager",
                    Description = "Oversees project progress and coordinates with teams"
                },
                new ProjectRole()
                {

                    Title = "Tester",
                    Description = "Tests and validates the project outputs"
                }
            );
            context.SaveChanges();
        }

        private static void AddAppRoles(AppDbContext context)
        {
            context.AppRoles.AddRange(
                new AppRole()
                {

                    Title = "Admin",
                    Description = "Administrator with full permissions"
                },
                new AppRole()
                {

                    Title = "User",
                    Description = "Regular user with limited permissions"
                }
            );
            context.SaveChanges();
        }

        private static void AddUsers(AppDbContext context)
        {
            context.Users.AddRange(
                new User()
                {
                    Username = "john.doe",
                    Email = "john.doe@example.com",
                    Password = "$2a$11$AplGAfsGlEFkxSviX4XP/.Rqgi6c0I4Lgr2PVns5CS.O/1rcsXWnO",
                    EntraObjectId = "entra-001",
                    AppRoleId = 1
                },
                new User()
                {
                    Username = "jane.smith",
                    Email = "jane.smith@example.com",
                    Password = "$2a$11$V7okUEf6msZqD6q.g8JynOiktb72Zxk19ww/zxa2ntTBMUS.15XuS",
                    EntraObjectId = "entra-002",
                    AppRoleId = 2
                },
                new User()
                {
                    Username = "alice.johnson",
                    Email = "alice.johnson@example.com",
                    Password = "$2a$11$AplGAfsGlEFkxSviX4XP/.Rqgi6c0I4Lgr2PVns5CS.O/1rcsXWnO",
                    EntraObjectId = "entra-003",
                    AppRoleId = 1
                }
            );
            context.SaveChanges();
        }

        private static void AddProjects(AppDbContext context)
        {
            context.Projects.AddRange(
                new Project()
                {
                    Title = "Project Alpha",
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = new DateOnly(2024, 12, 31),
                    Description = "A major project for 2024.",
                    IsFinished = false,
                    IsPrivate = true,
                    IsEducation = true,
                    IsPayable = true,
                    DevOpsProjectId = "alpha123"
                },
                new Project()
                {
                    Title = "Project Beta",
                    StartDate = new DateOnly(2024, 3, 1),
                    EndDate = new DateOnly(2024, 11, 30),
                    Description = "A beta version of our service.",
                    IsFinished = true,
                    IsPrivate = false,
                    IsEducation = false,
                    IsPayable = true,
                    DevOpsProjectId = "beta456"
                },
                new Project()
                {
                    Title = "Project Gamma",
                    StartDate = new DateOnly(2024, 6, 1),
                    EndDate = new DateOnly(2024, 9, 30),
                    Description = "A summer project for interns.",
                    IsFinished = false,
                    IsPrivate = false,
                    IsEducation = false,
                    IsPayable = true,
                    DevOpsProjectId = "gamma789"
                },
                new Project()
                {
                    Title = "Project Delta",
                    StartDate = new DateOnly(2024, 4, 1),
                    EndDate = new DateOnly(2025, 3, 31),
                    Description = "An extended project for developing new features.",
                    IsFinished = false,
                    IsPrivate = true,
                    IsEducation = false,
                    IsPayable = false,
                    DevOpsProjectId = "delta012"
                },
                new Project()
                {
                    Title = "Project Epsilon",
                    StartDate = new DateOnly(2023, 8, 15),
                    EndDate = new DateOnly(2024, 8, 14),
                    Description = "A research and development project.",
                    IsFinished = false,
                    IsPrivate = true,
                    IsEducation = true,
                    IsPayable = false,
                    DevOpsProjectId = "epsilon345"
                }
            );
            context.SaveChanges();
        }

        private static void AddUserProjectRoles(AppDbContext context)
        {
            context.UserProjectRoles.AddRange(
                new UserProjectRole()
                {
                    UserId = 1,
                    ProjectId = 1,
                    ProjectRoleId = 1
                },
                new UserProjectRole()
                {
                    UserId = 1,
                    ProjectId = 2,
                    ProjectRoleId = 2
                },
                new UserProjectRole()
                {
                    UserId = 1,
                    ProjectId = 3,
                    ProjectRoleId = 3
                },
                new UserProjectRole()
                {
                    UserId = 1,
                    ProjectId = 4,
                    ProjectRoleId = 3
                },
                new UserProjectRole()
                {
                    UserId = 1,
                    ProjectId = 5,
                    ProjectRoleId = 3
                },
                new UserProjectRole()
                {
                    UserId = 2,
                    ProjectId = 1,
                    ProjectRoleId = 1
                },
                new UserProjectRole()
                {
                    UserId = 2,
                    ProjectId = 2,
                    ProjectRoleId = 2
                },
                new UserProjectRole()
                {
                    UserId = 2,
                    ProjectId = 3,
                    ProjectRoleId = 3
                },
                new UserProjectRole()
                {
                    UserId = 2,
                    ProjectId = 4,
                    ProjectRoleId = 3
                },
                new UserProjectRole()
                {
                    UserId = 2,
                    ProjectId = 5,
                    ProjectRoleId = 3
                },
                new UserProjectRole()
                {
                    UserId = 3,
                    ProjectId = 1,
                    ProjectRoleId = 1
                },
                new UserProjectRole()
                {
                    UserId = 3,
                    ProjectId = 2,
                    ProjectRoleId = 2
                },
                new UserProjectRole()
                {
                    UserId = 3,
                    ProjectId = 3,
                    ProjectRoleId = 3
                },
                new UserProjectRole()
                {
                    UserId = 3,
                    ProjectId = 4,
                    ProjectRoleId = 2
                },
                new UserProjectRole()
                {
                    UserId = 3,
                    ProjectId = 5,
                    ProjectRoleId = 3
                }
            );
            context.SaveChanges();
        }

        private static void AddWorkDays(AppDbContext context)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            int currentMonth = today.Month;
            int currentYear = today.Year;

            int lastMonth = currentMonth == 1 ? 12 : currentMonth - 1;

            void AddWorkDaysForMonth(int year, int month)
            {
                DateOnly startDate = new DateOnly(year, month, 1);
                DateOnly endDate = startDate.AddMonths(1).AddDays(-1);

                for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    int userId = (date.Day % 3) + 1;
                    context.WorkDays.Add(new WorkDay
                    {
                        WorkDate = date,
                        UserId = userId
                    });
                }
            }

            AddWorkDaysForMonth(currentYear, lastMonth);
            AddWorkDaysForMonth(currentYear, currentMonth);

            context.SaveChanges();
        }

        private static void AddProjectTimes(AppDbContext context)
        {
            context.ProjectTimes.AddRange(
                new ProjectTime()
                {
                    Comment = "Initial setup and planning",
                    TimeSpentMinutes = 450,
                    WorkDayId = 1,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Development and testing",
                    TimeSpentMinutes = 460,
                    WorkDayId = 2,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Final review",
                    TimeSpentMinutes = 470,
                    WorkDayId = 3,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Design phase",
                    TimeSpentMinutes = 480,
                    WorkDayId = 4,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Code implementation",
                    TimeSpentMinutes = 455,
                    WorkDayId = 5,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Testing and debugging",
                    TimeSpentMinutes = 310,
                    WorkDayId = 6,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Client meeting and feedback",
                    TimeSpentMinutes = 290,
                    WorkDayId = 7,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Documentation",
                    TimeSpentMinutes = 120,
                    WorkDayId = 8,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Prototype development",
                    TimeSpentMinutes = 80,
                    WorkDayId = 9,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Integration with existing systems",
                    TimeSpentMinutes = 110,
                    WorkDayId = 10,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Performance optimization",
                    TimeSpentMinutes = 480,
                    WorkDayId = 11,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "User training and support",
                    TimeSpentMinutes = 455,
                    WorkDayId = 12,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Security assessment",
                    TimeSpentMinutes = 465,
                    WorkDayId = 13,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Feature enhancement",
                    TimeSpentMinutes = 475,
                    WorkDayId = 14,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Bug fixing",
                    TimeSpentMinutes = 450,
                    WorkDayId = 15,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "System upgrade",
                    TimeSpentMinutes = 460,
                    WorkDayId = 16,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Quality assurance",
                    TimeSpentMinutes = 470,
                    WorkDayId = 17,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Release preparation",
                    TimeSpentMinutes = 480,
                    WorkDayId = 18,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Code review",
                    TimeSpentMinutes = 455,
                    WorkDayId = 19,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Research and development",
                    TimeSpentMinutes = 465,
                    WorkDayId = 20,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Initial planning",
                    TimeSpentMinutes = 120,
                    WorkDayId = 21,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Database design",
                    TimeSpentMinutes = 150,
                    WorkDayId = 22,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Front-end development",
                    TimeSpentMinutes = 240,
                    WorkDayId = 23,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Bug fixing",
                    TimeSpentMinutes = 300,
                    WorkDayId = 24,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "User feedback analysis",
                    TimeSpentMinutes = 200,
                    WorkDayId = 25,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Performance optimization",
                    TimeSpentMinutes = 350,
                    WorkDayId = 26,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Documentation",
                    TimeSpentMinutes = 180,
                    WorkDayId = 27,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Server maintenance",
                    TimeSpentMinutes = 230,
                    WorkDayId = 28,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Design mockups",
                    TimeSpentMinutes = 140,
                    WorkDayId = 29,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "API integration",
                    TimeSpentMinutes = 260,
                    WorkDayId = 30,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Usability testing",
                    TimeSpentMinutes = 320,
                    WorkDayId = 31,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Client meeting",
                    TimeSpentMinutes = 90,
                    WorkDayId = 32,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Feature implementation",
                    TimeSpentMinutes = 220,
                    WorkDayId = 33,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Deployment",
                    TimeSpentMinutes = 400,
                    WorkDayId = 34,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Backend development",
                    TimeSpentMinutes = 150,
                    WorkDayId = 35,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Security review",
                    TimeSpentMinutes = 190,
                    WorkDayId = 36,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Integration testing",
                    TimeSpentMinutes = 270,
                    WorkDayId = 37,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Onboarding new team members",
                    TimeSpentMinutes = 200,
                    WorkDayId = 38,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Code refactoring",
                    TimeSpentMinutes = 250,
                    WorkDayId = 39,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "API documentation",
                    TimeSpentMinutes = 300,
                    WorkDayId = 40,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Prototyping",
                    TimeSpentMinutes = 110,
                    WorkDayId = 41,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Load testing",
                    TimeSpentMinutes = 220,
                    WorkDayId = 42,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Client feedback review",
                    TimeSpentMinutes = 180,
                    WorkDayId = 43,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "User interface design",
                    TimeSpentMinutes = 250,
                    WorkDayId = 44,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Database migration",
                    TimeSpentMinutes = 310,
                    WorkDayId = 45,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "System monitoring",
                    TimeSpentMinutes = 130,
                    WorkDayId = 46,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Bug tracking",
                    TimeSpentMinutes = 160,
                    WorkDayId = 47,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Feature testing",
                    TimeSpentMinutes = 210,
                    WorkDayId = 48,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Database optimization",
                    TimeSpentMinutes = 280,
                    WorkDayId = 49,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "System diagnostics",
                    TimeSpentMinutes = 190,
                    WorkDayId = 50,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Technical debt reduction",
                    TimeSpentMinutes = 230,
                    WorkDayId = 51,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Stakeholder meeting",
                    TimeSpentMinutes = 140,
                    WorkDayId = 52,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Knowledge sharing",
                    TimeSpentMinutes = 170,
                    WorkDayId = 53,
                    ProjectId = 1
                },
                new ProjectTime()
                {
                    Comment = "Code documentation",
                    TimeSpentMinutes = 240,
                    WorkDayId = 54,
                    ProjectId = 2
                },
                new ProjectTime()
                {
                    Comment = "Infrastructure setup",
                    TimeSpentMinutes = 250,
                    WorkDayId = 55,
                    ProjectId = 3
                },
                new ProjectTime()
                {
                    Comment = "Project review",
                    TimeSpentMinutes = 200,
                    WorkDayId = 56,
                    ProjectId = 4
                },
                new ProjectTime()
                {
                    Comment = "Testing automation",
                    TimeSpentMinutes = 300,
                    WorkDayId = 57,
                    ProjectId = 1
                }
            );
            context.SaveChanges();
        }

        private void AddAnnualLeaves(AppDbContext context)
        {
            context.AnnualLeaves.AddRange(
                new AnnualLeave()
                {
                    Year = 2024,
                    TotalLeaveDays = 20,
                    LeftLeaveDays = 16,
                    UserId = 1
                },
                new AnnualLeave()
                {
                    Year = 2024,
                    TotalLeaveDays = 18,
                    LeftLeaveDays = 14,
                    UserId = 2
                },
                new AnnualLeave()
                {
                    Year = 2024,
                    TotalLeaveDays = 22,
                    LeftLeaveDays = 18,
                    UserId = 3
                },
                new AnnualLeave()
                {
                    Year = 2023,
                    TotalLeaveDays = 20,
                    LeftLeaveDays = 2,
                    UserId = 1
                },
                new AnnualLeave()
                {
                    Year = 2023,
                    TotalLeaveDays = 18,
                    LeftLeaveDays = 1,
                    UserId = 2
                },
                new AnnualLeave()
                {
                    Year = 2023,
                    TotalLeaveDays = 22,
                    LeftLeaveDays = 0,
                    UserId = 3
                }
            );

            context.SaveChanges();
        }

        private void AddAnnualLeavesRecords(AppDbContext context)
        {
            context.AnnualLeavesRecords.AddRange(
               new AnnualLeaveRecord()
               {
                   StartDate = new DateOnly(2024, 8, 7),
                   EndDate = new DateOnly(2024, 8, 14),
                   Comment = "A research",
                   UserId = 1
               },
               new AnnualLeaveRecord()
               {
                   StartDate = new DateOnly(2024, 8, 8),
                   EndDate = new DateOnly(2024, 8, 14),
                   Comment = "A research",
                   UserId = 2
               },
               new AnnualLeaveRecord()
               {
                   StartDate = new DateOnly(2024, 8, 9),
                   EndDate = new DateOnly(2024, 8, 14),
                   Comment = "A research",
                   UserId = 3
               }
            );

            context.SaveChanges();
        }

        private void AddSickLeaveRecords(AppDbContext context)
        {
            context.SickLeaveRecords.AddRange(
               new SickLeaveRecord()
               {
                   StartDate = new DateOnly(2024, 2, 2),
                   EndDate = new DateOnly(2024, 2, 4),
                   Comment = "A research",
                   UserId = 1
               },
                new SickLeaveRecord()
                {
                    StartDate = new DateOnly(2024, 3, 2),
                    EndDate = new DateOnly(2024, 3, 4),
                    Comment = "A research",
                    UserId = 2
                },
                 new SickLeaveRecord()
                 {
                     StartDate = new DateOnly(2024, 4, 2),
                     EndDate = new DateOnly(2024, 4, 4),
                     Comment = "A research",
                     UserId = 3
                 }
            );

            context.SaveChanges();
        }
    }
}
