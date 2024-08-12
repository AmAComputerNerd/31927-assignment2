using HotelSmartManagement.Common.Database.Misc;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.Models
{
    public class LeaveRequest : IDatabaseObject
    {
#nullable disable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
        public Guid UniqueId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }

        public Guid EmployeeDetailsId { get; set; }
        public EmployeeDetails EmployeeDetails { get; set; }
#nullable enable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
    }
}
