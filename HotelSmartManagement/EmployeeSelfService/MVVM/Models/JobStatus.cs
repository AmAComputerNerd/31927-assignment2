namespace HotelSmartManagement.EmployeeSelfService.MVVM.Models
{
    public enum JobStatus
    {
        Pending,
        Assigned,
        InProgress,
        Completed,
        Cancelled
    }

    public static class JobStatusExtensions
    {
        public static string ToFriendlyString(this JobStatus status)
        {
            return status switch
            {
                JobStatus.Pending => "Pending Allocation",
                JobStatus.Assigned => "Assigned",
                JobStatus.InProgress => "In Progress",
                JobStatus.Completed => "Completed",
                JobStatus.Cancelled => "Cancelled",
                _ => "Unknown",
            };
        }

        public static string GetDescription(this JobStatus status)
        {
            return status switch
            {
                JobStatus.Pending => "The job is pending allocation.",
                JobStatus.Assigned => "The job has been assigned to an employee.",
                JobStatus.InProgress => "The job is currently being worked on.",
                JobStatus.Completed => "The job has been completed.",
                JobStatus.Cancelled => "The job has been cancelled.",
                _ => "Unknown",
            };
        }
    }
}
