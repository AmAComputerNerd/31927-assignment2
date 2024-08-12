using HotelSmartManagement.Common.Database.Misc;
using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.Models
{
    public class EmployeeDetails : IDatabaseObject
    {
#nullable disable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
        public Guid UniqueId { get; set; }
        public int BankAccountNo { get; set; }
        public int BankAccountBSB { get; set; }
        public string JobPosition { get; set; }
        public EmployeeStatus JobStatus { get; set; }
        public int? JobHoursPerWeek { get; set; }
        public double JobPayPerHour { get; set; }
        public double? LeaveBalanceInHours { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
#nullable enable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
    }
}
