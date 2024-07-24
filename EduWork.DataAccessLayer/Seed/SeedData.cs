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
                    EntraObjectId = "entra-001",
                    AppRoleId = 1
                },
                new User()
                {
                    Username = "jane.smith",
                    Email = "jane.smith@example.com",
                    EntraObjectId = "entra-002",
                    AppRoleId = 2
                },
                new User()
                {
                    Username = "alice.johnson",
                    Email = "alice.johnson@example.com",
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
                    IsEducation = true,
                    IsPayable = false,
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
                    IsPayable = true,
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
                },
                new Project()
                {
                    Title = "Project Zeta",
                    StartDate = new DateOnly(2024, 2, 1),
                    EndDate = new DateOnly(2024, 12, 1),
                    Description = "A special initiative for environmental sustainability.",
                    IsFinished = false,
                    IsPrivate = false,
                    IsEducation = true,
                    IsPayable = true,
                    DevOpsProjectId = "zeta678"
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
                    UserId = 2,
                    ProjectId = 2,
                    ProjectRoleId = 2
                },
                new UserProjectRole()
                {
                    UserId = 3,
                    ProjectId = 3,
                    ProjectRoleId = 3
                }
            );
            context.SaveChanges();
        }
        private static void AddWorkDays(AppDbContext context)
        {
            context.WorkDays.AddRange(
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 1),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 2),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 3),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 4),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 5),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 6),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 7),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 8),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 9),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 10),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 11),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 12),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 13),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 14),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 15),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 16),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 17),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 18),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 19),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 20),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 21),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 22),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 23),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 24),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 25),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 26),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 27),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 28),
                    UserId = 3
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 29),
                    UserId = 2
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 30),
                    UserId = 1
                },
                new WorkDay()
                {
                    WorkDate = new DateOnly(2024, 7, 31),
                    UserId = 2
                }
            );
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
                    ProjectId = 5
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
                    ProjectId = 5
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
                    ProjectId = 5
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
                    ProjectId = 5
                }
            );
            context.SaveChanges();
        }
    }
}
