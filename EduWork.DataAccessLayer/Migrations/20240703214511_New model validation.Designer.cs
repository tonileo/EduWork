﻿// <auto-generated />
using System;
using EduWork.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EduWork.DataAccessLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240703214511_New model validation")]
    partial class Newmodelvalidation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.AnnualLeave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LeftLeaveDays")
                        .HasColumnType("int");

                    b.Property<int>("TotalLeaveDays")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Year")
                        .IsUnique();

                    b.ToTable("AnnualLeaves");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.AnnualLeaveRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AnnualLeavesRecords");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.NonWorkingDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("NonWorkingDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NonWorkingDate")
                        .IsUnique();

                    b.ToTable("NonWorkingDays");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DevOpsProjectId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsEducation")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPayable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.ProjectRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("ProjectRoles");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.ProjectTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("TimeSpentMinutes")
                        .HasColumnType("int");

                    b.Property<int>("WorkDayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("WorkDayId");

                    b.ToTable("ProjectTimes");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.SickLeaveRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SickLeaveRecords");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EntraObjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AppRoleId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EntraObjectId")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.UserProjectRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectRoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectRoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProjectRoles");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.WorkDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("WorkDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "WorkDate")
                        .IsUnique();

                    b.ToTable("WorkDays");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.WorkDayTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.Property<int>("WorkDayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkDayId");

                    b.ToTable("WorkDayTimes");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.AnnualLeave", b =>
                {
                    b.HasOne("EduWork.DataAccessLayer.Entites.User", "User")
                        .WithMany("AnnualLeaves")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.AnnualLeaveRecord", b =>
                {
                    b.HasOne("EduWork.DataAccessLayer.Entites.User", "User")
                        .WithMany("AnnualLeaveRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.ProjectTime", b =>
                {
                    b.HasOne("EduWork.DataAccessLayer.Entites.Project", "Project")
                        .WithMany("ProjectTimes")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduWork.DataAccessLayer.Entites.WorkDay", "WorkDay")
                        .WithMany("ProjectTimes")
                        .HasForeignKey("WorkDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("WorkDay");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.SickLeaveRecord", b =>
                {
                    b.HasOne("EduWork.DataAccessLayer.Entites.User", "User")
                        .WithMany("SickLeaveRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.User", b =>
                {
                    b.HasOne("EduWork.DataAccessLayer.Entites.AppRole", "AppRole")
                        .WithMany("Users")
                        .HasForeignKey("AppRoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AppRole");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.UserProjectRole", b =>
                {
                    b.HasOne("EduWork.DataAccessLayer.Entites.Project", "Project")
                        .WithMany("UserProjectRoles")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduWork.DataAccessLayer.Entites.ProjectRole", "ProjectRole")
                        .WithMany("UserProjectRoles")
                        .HasForeignKey("ProjectRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduWork.DataAccessLayer.Entites.User", "User")
                        .WithMany("UserProjectRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("ProjectRole");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.WorkDay", b =>
                {
                    b.HasOne("EduWork.DataAccessLayer.Entites.User", "User")
                        .WithMany("WorkDays")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.WorkDayTime", b =>
                {
                    b.HasOne("EduWork.DataAccessLayer.Entites.WorkDay", "WorkDay")
                        .WithMany("WorkDayTimes")
                        .HasForeignKey("WorkDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkDay");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.AppRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.Project", b =>
                {
                    b.Navigation("ProjectTimes");

                    b.Navigation("UserProjectRoles");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.ProjectRole", b =>
                {
                    b.Navigation("UserProjectRoles");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.User", b =>
                {
                    b.Navigation("AnnualLeaveRecords");

                    b.Navigation("AnnualLeaves");

                    b.Navigation("SickLeaveRecords");

                    b.Navigation("UserProjectRoles");

                    b.Navigation("WorkDays");
                });

            modelBuilder.Entity("EduWork.DataAccessLayer.Entites.WorkDay", b =>
                {
                    b.Navigation("ProjectTimes");

                    b.Navigation("WorkDayTimes");
                });
#pragma warning restore 612, 618
        }
    }
}