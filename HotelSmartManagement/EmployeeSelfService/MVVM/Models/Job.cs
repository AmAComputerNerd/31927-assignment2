using HotelSmartManagement.Common.Database.Misc;
using HotelSmartManagement.Common.MVVM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.Models
{
    public class Job : IDatabaseObject
    {
#nullable disable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
        [Key]
        public Guid UniqueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double TimeLoggedWorking { get; set; }
        public JobUrgencyLevel UrgencyLevel { get; set; }
        public JobType TaskType { get; set; }
        public JobStatus Status { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public Guid CreatedById { get; set; }
        [ForeignKey(nameof(CreatedById))]
        [InverseProperty(nameof(User.CreatedJobs))]
        public User CreatedBy { get; set; }
#nullable enable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.

        public Guid? AssignedToId { get; set; }
        [ForeignKey(nameof(AssignedToId))]
        [InverseProperty(nameof(User.AssignedJobs))]
        public User? AssignedTo { get; set; }

        public DateTime? ClosedAtUtc { get; set; }
        public Guid? ClosedById { get; set; }
        [ForeignKey(nameof(ClosedById))]
        [InverseProperty(nameof(User.ClosedJobs))]
        public User? ClosedBy { get; set; }
    }
}
