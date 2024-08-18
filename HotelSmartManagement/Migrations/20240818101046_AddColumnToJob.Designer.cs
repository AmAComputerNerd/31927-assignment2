﻿// <auto-generated />
using System;
using HotelSmartManagement.Common.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelSmartManagement.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    [Migration("20240818101046_AddColumnToJob")]
    partial class AddColumnToJob
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
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

                    b.Property<double>("TimeLoggedWorking")
                        .HasColumnType("float");

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

                    b.Property<int>("Category")
                        .HasColumnType("int");

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

                    b.Property<int>("AreaAffected")
                        .HasColumnType("int");

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

            modelBuilder.Entity("HotelSmartManagement.ReservationAndRooms.MVVM.Models.Guest", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stays")
                        .HasColumnType("int");

                    b.Property<string>("Tier")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UniqueId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("HotelSmartManagement.ReservationAndRooms.MVVM.Models.Reservation", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GuestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requests")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UniqueId");

                    b.HasIndex("GuestId")
                        .IsUnique()
                        .HasFilter("[GuestId] IS NOT NULL");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("HotelSmartManagement.ReservationAndRooms.MVVM.Models.Room", b =>
                {
                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Amenities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Layout")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("UniqueId");

                    b.ToTable("Rooms");
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

            modelBuilder.Entity("HotelSmartManagement.ReservationAndRooms.MVVM.Models.Reservation", b =>
                {
                    b.HasOne("HotelSmartManagement.ReservationAndRooms.MVVM.Models.Guest", "Guest")
                        .WithOne("Reservation")
                        .HasForeignKey("HotelSmartManagement.ReservationAndRooms.MVVM.Models.Reservation", "GuestId");

                    b.HasOne("HotelSmartManagement.ReservationAndRooms.MVVM.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.Navigation("Guest");

                    b.Navigation("Room");
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

            modelBuilder.Entity("HotelSmartManagement.ReservationAndRooms.MVVM.Models.Guest", b =>
                {
                    b.Navigation("Reservation");
                });
#pragma warning restore 612, 618
        }
    }
}
