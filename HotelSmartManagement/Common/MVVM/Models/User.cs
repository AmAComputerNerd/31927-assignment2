using HotelSmartManagement.Common.Database.Misc;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSmartManagement.Common.MVVM.Models
{
    public class User : IDatabaseObject
    {
#nullable disable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
        [Key]
        public Guid UniqueId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfilePictureFileName { get; set; }

        public Guid EmployeeDetailsId { get; set; }
        [InverseProperty(nameof(EmployeeDetails.User))]
        public EmployeeDetails EmployeeDetails { get; set; }

        [InverseProperty(nameof(Job.CreatedBy))]
        public ICollection<Job> CreatedJobs { get; set; }
        [InverseProperty(nameof(Job.AssignedTo))]
        public ICollection<Job> AssignedJobs { get; set; }
        [InverseProperty(nameof(Job.ClosedBy))]
        public ICollection<Job> ClosedJobs { get; set; }
#nullable enable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
    }
}
