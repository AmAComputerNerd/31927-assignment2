using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.SubWindows;
using System.Collections.ObjectModel;
using System.Windows;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class EmployeeSelfServiceDashboardViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(EmployeeSelfServiceDashboardViewModel);

        // Private fields.
        private readonly UserService _userService;
        private readonly JobService _jobService;
        private Uri _imageUri;
        private string _username;
        private double _jobHoursPerWeek;
        private double _jobActualHoursThisWeek;
        private double _leaveBalanceInHours;
        private double _userTimeRecording;
        private ObservableCollection<Job> _assignedJobs;
        private int _numberOfAssignedJobs;
        private int _highestUrgencyLevelOfAssignedJobs;

        // Public properties.
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        public double JobHoursPerWeek { get => _jobHoursPerWeek; set => SetProperty(ref _jobHoursPerWeek, value); }
        public double JobActualHoursThisWeek { get => _jobActualHoursThisWeek; set => SetProperty(ref _jobActualHoursThisWeek, value); }
        public double LeaveBalanceInHours { get => _leaveBalanceInHours; set => SetProperty(ref _leaveBalanceInHours, value); }
        public double UserTimeRecording { get => _userTimeRecording; set => SetProperty(ref _userTimeRecording, value); }
        public ObservableCollection<Job> AssignedJobs { get => _assignedJobs; set => SetProperty(ref _assignedJobs, value); }
        public int NumberOfAssignedJobs { get => _numberOfAssignedJobs; set => SetProperty(ref _numberOfAssignedJobs, value); }
        public int HighestUrgencyLevelOfAssignedJobs { get => _highestUrgencyLevelOfAssignedJobs; set => SetProperty(ref _highestUrgencyLevelOfAssignedJobs, value); }

        // Commands.
        public AsyncRelayCommand OnMyProfile_Clicked { get; }
        public AsyncRelayCommand OnMyEmployment_Clicked { get; }
        public AsyncRelayCommand OnViewJob_Clicked { get; }
        public AsyncRelayCommand OnAddJob_Clicked { get; }
        public AsyncRelayCommand OnCloseJob_Clicked { get; }
        public AsyncRelayCommand OnCancelJob_Clicked { get; }

#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public EmployeeSelfServiceDashboardViewModel(Globals globals, UserService userService, JobService jobService) : base(globals)
#pragma warning restore CS8618 // Reason: private fields are set through public properties.
        {
            _userService = userService;
            _jobService = jobService;

            OnMyProfile_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(EmployeeSelfServiceMyDetailsViewModel)), nameof(MainViewModel))));
            OnMyEmployment_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(EmployeeSelfServiceMyDetailsViewModel)), nameof(MainViewModel))));
            OnViewJob_Clicked = new AsyncRelayCommand(async () =>
            {
                // Get the selected job.
                // Send a CreateOrDestroySubWindowEvent to create a new JobWindow, with the argument of the selected job.
            });
            OnAddJob_Clicked = new AsyncRelayCommand(async () =>
            {
                // Send a CreateOrDestroySubWindowEvent to create a new JobWindow with no argument to indicate we are adding a new job.
                var window = await Messenger.Send(CreateOrDestroySubWindowEvent.CreateWindow(typeof(JobWindow), typeof(JobWindowViewModel))).Response;
                window.Show();
            });
            OnCloseJob_Clicked = new AsyncRelayCommand(async () =>
            {
                // Get the selected job.
                // Set its status to JobStatus.Closed.
                // Update the job in the database.
            });
            OnCancelJob_Clicked = new AsyncRelayCommand(async () =>
            {
                // Get the selected job.
                // Set its status to JobStatus.Cancelled.
                // Update the job in the database.
            });
            RefreshUserBindings();
        }

        protected override void OnActivated()
        {
            Messenger.Register<EmployeeSelfServiceDashboardViewModel, UserChangedEvent>(this, (receiver, message) => receiver.UserChanged(message));
            Messenger.Register<EmployeeSelfServiceDashboardViewModel, JobChangedEvent>(this, (receiver, message) => receiver.JobChanged(message));
        }

        private void UserChanged(UserChangedEvent @event)
        {
            MessageBox.Show("User has changed or logged out. Please reload this page to continue editing!\nYour changes will not be apply unless you log back in!", "Concurrency > User Changed", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshUserBindings();
        }

        private async void JobChanged(JobChangedEvent @event)
        {
            // Retrieve the job from the database
            var jobFromDb = _jobService.GetJob(@event.JobId);

            if (jobFromDb == null)
            {
                // Job was deleted
                // Attempt to find and remove the job from the assigned jobs
                var jobToRemove = AssignedJobs.FirstOrDefault(j => j.UniqueId == @event.JobId);
                if (jobToRemove != null)
                {
                    AssignedJobs.Remove(jobToRemove);
                }
            }
            else
            {
                // Job exists in the database
                var existingJob = AssignedJobs.FirstOrDefault(j => j.UniqueId == jobFromDb.UniqueId);

                if (existingJob != null)
                {
                    // Job is already in the assigned jobs list
                    if (jobFromDb.Status == JobStatus.Assigned && jobFromDb.AssignedTo?.UniqueId == Globals.CurrentUser?.UniqueId)
                    {
                        // Job status and assignment haven't changed, update the job
                        var index = AssignedJobs.IndexOf(existingJob);
                        AssignedJobs[index] = jobFromDb;
                    }
                    else
                    {
                        // Job status or assignment has changed, remove the job
                        AssignedJobs.Remove(existingJob);
                    }
                }
                else if (jobFromDb.Status == JobStatus.Assigned && jobFromDb.AssignedTo?.UniqueId == Globals.CurrentUser?.UniqueId)
                {
                    // Job is not in the list but is assigned to the current user, add it
                    AssignedJobs.Add(jobFromDb);
                }
            }
        }

        private void RefreshUserBindings()
        {
            Username = Globals.CurrentUser?.Username ?? "DESIGNER";
            JobHoursPerWeek = Globals.CurrentUser?.EmployeeDetails?.JobHoursPerWeek ?? 0;
            JobActualHoursThisWeek = Globals.CurrentUser?.EmployeeDetails?.JobActualHoursThisWeek ?? 0;
            LeaveBalanceInHours = Globals.CurrentUser?.EmployeeDetails?.LeaveBalanceInHours ?? 0;
            UserTimeRecording = Math.Round((Globals.CurrentUser?.EmployeeDetails?.JobActualHoursThisWeek ?? 0) / (double)(Globals.CurrentUser?.EmployeeDetails?.JobHoursPerWeek ?? 1) * 100, 2);
            AssignedJobs = new ObservableCollection<Job>(Globals.CurrentUser?.AssignedJobs ?? []);
            NumberOfAssignedJobs = AssignedJobs.Count;
            HighestUrgencyLevelOfAssignedJobs = NumberOfAssignedJobs == 0 ? 0 : AssignedJobs.Max(job => (int)job.UrgencyLevel);
        }
    }
}
