using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.IO;

namespace HotelSmartManagement.Common.MVVM.Models
{
    public class Globals
    {
        public string AzureBlobConnectionString { get; }
        public IConfiguration Configuration { get; }
        public User? CurrentUser { get; set; }

        public Globals(IConfiguration configuration)
        {
            AzureBlobConnectionString = configuration.GetConnectionString("AzureBlobConnection") ?? throw new ArgumentException("Configuration does not have an AzureBlobConnection section.");
            Configuration = configuration;
            //CurrentUser = null;
            CurrentUser = new User()
            {
                Username = "Test123",
                Password = "12345",
                Email = "test12345@gmail.com",
                ProfilePictureFileName = "test123.png",
                CreatedJobs = new Collection<Job>(),
                AssignedJobs = new Collection<Job>()
                {
                    new Job()
                    {
                        Title = "Clean the kitchen",
                        Description = "The kitchen on Floor 1 is a mess. And we know you did it. Clean it up.",
                        UrgencyLevel = JobUrgencyLevel.Medium,
                        TaskType = JobType.Maintenance,
                        Status = JobStatus.Pending,
                        CreatedAtUtc = DateTime.Now,
                    }
                },
                ClosedJobs = new Collection<Job>(),
                EmployeeDetails = new EmployeeDetails()
                {
                    BankAccountNo = 1008_8392,
                    BankAccountBSB = 027_893,
                    JobPosition = "Senior Administrator",
                    JobStatus = EmployeeStatus.FullTime,
                    JobHoursPerWeek = 38,
                    JobActualHoursThisWeek = 33,
                    JobPayPerHour = 60,
                    LeaveBalanceInHours = 24,
                    LeaveRequests = new Collection<LeaveRequest>()
                    {
                        new LeaveRequest()
                        {
                            StartAt = DateTime.MinValue,
                            EndAt = DateTime.Now,
                            Description = "Hello there",
                            IsApproved = true
                        },
                        new LeaveRequest()
                        {
                            StartAt = DateTime.Now,
                            EndAt = DateTime.MaxValue,
                            Description = "General Kenobi...",
                            IsApproved = false
                        }
                    }
                }
            };
        }

        public Uri GetProfilePictureUri()
        {
            Uri.TryCreate(Path.Combine(AzureBlobConnectionString, CurrentUser?.ProfilePictureFileName ?? "unknown-user.png"), UriKind.Absolute, out var profilePicturePath);
            if (profilePicturePath == null)
            {
                throw new ArgumentException("ProfilePicture was invalid or missing, and the backup image could not be found either. Check your Azure connection settings.");
            }
            return profilePicturePath;
        }
    }
}
