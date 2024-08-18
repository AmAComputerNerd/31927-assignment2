﻿// <auto-generated />
using System;
using HotelSmartManagement.Common.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelSmartManagement.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    partial class HotelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelSmartManagement.Common.MVVM.Models.User", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmployeeDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UniqueId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HotelSmartManagement.EmployeeSelfService.MVVM.Models.EmployeeDetails", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BankAccountBSB")
                        .HasColumnType("int");

                    b.Property<int>("BankAccountNo")
                        .HasColumnType("int");

                    b.Property<int>("JobActualHoursThisWeek")
                        .HasColumnType("int");

                    b.Property<int?>("JobHoursPerWeek")
                        .HasColumnType("int");

                    b.Property<double>("JobPayPerHour")
                        .HasColumnType("float");

                    b.Property<string>("JobPosition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobStatus")
                        .HasColumnType("int");

                    b.Property<double?>("LeaveBalanceInHours")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UniqueId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("EmployeeDetails");
                });

            modelBuilder.Entity("HotelSmartManagement.EmployeeSelfService.MVVM.Models.Job", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssignedToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ClosedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ClosedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TaskType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UrgencyLevel")
                        .HasColumnType("int");

                    b.HasKey("UniqueId");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("ClosedById");

                    b.HasIndex("CreatedById");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("HotelSmartManagement.EmployeeSelfService.MVVM.Models.LeaveRequest", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmployeeDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UniqueId");

                    b.HasIndex("EmployeeDetailsId");

                    b.ToTable("LeaveRequests");
                });

            modelBuilder.Entity("HotelSmartManagement.HotelOverview.MVVM.Models.Announcement", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UniqueId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("HotelSmartManagement.HotelOverview.MVVM.Models.Event", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UniqueId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("HotelSmartManagement.HotelOverview.MVVM.Models.InventoryItem", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("UniqueId");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("HotelSmartManagement.EmployeeSelfService.MVVM.Models.EmployeeDetails", b =>
                {
                    b.HasOne("HotelSmartManagement.Common.MVVM.Models.User", "User")
                        .WithOne("EmployeeDetails")
                        .HasForeignKey("HotelSmartManagement.EmployeeSelfService.MVVM.Models.EmployeeDetails", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelSmartManagement.EmployeeSelfService.MVVM.Models.Job", b =>
                {
                    b.HasOne("HotelSmartManagement.Common.MVVM.Models.User", "AssignedTo")
                        .WithMany("AssignedJobs")
                        .HasForeignKey("AssignedToId");

                    b.HasOne("HotelSmartManagement.Common.MVVM.Models.User", "ClosedBy")
                        .WithMany("ClosedJobs")
                        .HasForeignKey("ClosedById");

                    b.HasOne("HotelSmartManagement.Common.MVVM.Models.User", "CreatedBy")
                        .WithMany("CreatedJobs")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("ClosedBy");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("HotelSmartManagement.EmployeeSelfService.MVVM.Models.LeaveRequest", b =>
                {
                    b.HasOne("HotelSmartManagement.EmployeeSelfService.MVVM.Models.EmployeeDetails", "EmployeeDetails")
                        .WithMany("LeaveRequests")
                        .HasForeignKey("EmployeeDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeDetails");
                });

            modelBuilder.Entity("HotelSmartManagement.Common.MVVM.Models.User", b =>
                {
                    b.Navigation("AssignedJobs");

                    b.Navigation("ClosedJobs");

                    b.Navigation("CreatedJobs");

                    b.Navigation("EmployeeDetails");
                });

            modelBuilder.Entity("HotelSmartManagement.EmployeeSelfService.MVVM.Models.EmployeeDetails", b =>
                {
                    b.Navigation("LeaveRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
