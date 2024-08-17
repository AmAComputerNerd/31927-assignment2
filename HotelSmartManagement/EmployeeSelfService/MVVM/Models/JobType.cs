namespace HotelSmartManagement.EmployeeSelfService.MVVM.Models
{
    public enum JobType
    {
        Reservation,
        Maintenance,
        Office,
        Other
    }

    public static class JobTypeExtensions
    {
        public static string ToFriendlyString(this JobType jobType)
        {
            return jobType switch
            {
                JobType.Reservation => "Reservation Task",
                JobType.Maintenance => "Maintenance Task",
                JobType.Office => "Office Task",
                JobType.Other => "Other Task",
                _ => "Unknown Task"
            };
        }

        public static string GetCapability(this JobType jobType)
        {
            return jobType switch
            {
                JobType.Reservation => "RES",
                JobType.Maintenance => "MNT",
                JobType.Office => "OFF",
                JobType.Other => "OTH",
                _ => "UNK"
            };
        }

        public static string GetDescription(this JobType jobType)
        {
            return jobType switch
            {
                JobType.Reservation => "Staff trained or in a role to handle reservations and interact with guests.",
                JobType.Maintenance => "Staff trained or in a role to handle maintenance, repairs and cleaning.",
                JobType.Office => "Staff trained or in a role to handle administrative tasks, paperwork and office work.",
                JobType.Other => "Staff trained or in a role to handle other tasks not covered by the other types.",
                _ => "Unknown job type."
            };
        }
    }
}
