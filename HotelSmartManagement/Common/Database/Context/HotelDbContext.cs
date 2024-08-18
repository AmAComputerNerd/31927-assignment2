using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using HotelSmartManagement.HotelOverview.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSmartManagement.Common.Database.Context
{
    public class HotelDbContext : DbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; }
        public DbSet<LeaveRequest> LeaveRequests { get; }
        public DbSet<Job> Jobs { get; }
        public DbSet<Announcement> Announcements { get; }
        public DbSet<Event> Events { get; }
        public DbSet<InventoryItem> InventoryItems { get; }
        public DbSet<Reservation> Reservations { get; }
        public DbSet<Guest> Guests { get; }
        public DbSet<Room> Rooms { get; }

#pragma warning disable CS8618 // Reason: DbSets are populated by EF.
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }
#pragma warning restore CS8618 // Reason: DbSets are populated by EF.
    }
}
