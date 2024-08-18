using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.SubWindows;
using System.Windows;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class JobWindowNewJobViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(JobWindowNewJobViewModel);

        // Fields
        private readonly UserService _userService;
        private readonly JobService _jobService;
        private string _jobTitle;
        private string _jobDescription;
        private bool _isJobUrgencyTrivialSelected;
        private bool _isJobUrgencyLowSelected;
        private bool _isJobUrgencyMediumSelected;
        private bool _isJobUrgencyHighSelected;
        private bool _isJobUrgencyCriticalSelected;
        private bool _isJobTypeReservationSelected;
        private bool _isJobTypeMaintenanceSelected;
        private bool _isJobTypeOfficeSelected;
        private bool _isJobTypeOtherSelected;
        private string? _assignedEmployeeUsername;

        public string JobTitle { get => _jobTitle; set => SetProperty(ref _jobTitle, value); }
        public string JobDescription { get => _jobDescription; set => SetProperty(ref _jobDescription, value); }
        public bool IsJobUrgencyTrivialSelected { get => _isJobUrgencyTrivialSelected; set => SetProperty(ref _isJobUrgencyTrivialSelected, value); }
        public bool IsJobUrgencyLowSelected { get => _isJobUrgencyLowSelected; set => SetProperty(ref _isJobUrgencyLowSelected, value); }
        public bool IsJobUrgencyMediumSelected { get => _isJobUrgencyMediumSelected; set => SetProperty(ref _isJobUrgencyMediumSelected, value); }
        public bool IsJobUrgencyHighSelected { get => _isJobUrgencyHighSelected; set => SetProperty(ref _isJobUrgencyHighSelected, value); }
        public bool IsJobUrgencyCriticalSelected { get => _isJobUrgencyCriticalSelected; set => SetProperty(ref _isJobUrgencyCriticalSelected, value); }
        public bool IsJobTypeReservationSelected { get => _isJobTypeReservationSelected; set => SetProperty(ref _isJobTypeReservationSelected, value); }
        public bool IsJobTypeMaintenanceSelected { get => _isJobTypeMaintenanceSelected; set => SetProperty(ref _isJobTypeMaintenanceSelected, value); }
        public bool IsJobTypeOfficeSelected { get => _isJobTypeOfficeSelected; set => SetProperty(ref _isJobTypeOfficeSelected, value); }
        public bool IsJobTypeOtherSelected { get => _isJobTypeOtherSelected; set => SetProperty(ref _isJobTypeOtherSelected, value); }
        public string? AssignedEmployeeUsername { get => _assignedEmployeeUsername; set => SetProperty(ref _assignedEmployeeUsername, value); }

        public AsyncRelayCommand OnSaveAndClose_Clicked { get; }
        public AsyncRelayCommand OnCancel_Clicked { get; }

#pragma warning disable CS8618 // Reason: fields are set through public properties.
        public JobWindowNewJobViewModel(UserService userService, JobService jobService, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: fields are set through public properties.
        {
            _userService = userService;
            _jobService = jobService;

            OnSaveAndClose_Clicked = new AsyncRelayCommand(async () => await SaveAndClose());
            OnCancel_Clicked = new AsyncRelayCommand(async () => await Cancel());

            JobTitle = string.Empty;
            JobDescription = string.Empty;
            // JobUrgency radio buttons
            IsJobUrgencyTrivialSelected = true;
            IsJobUrgencyLowSelected = false;
            IsJobUrgencyMediumSelected = false;
            IsJobUrgencyHighSelected = false;
            IsJobUrgencyCriticalSelected = false;
            // JobType radio buttons
            IsJobTypeReservationSelected = true;
            IsJobTypeMaintenanceSelected = false;
            IsJobTypeOfficeSelected = false;
            IsJobTypeOtherSelected = false;
            // Dropbox selection
            AssignedEmployeeUsername = null;
        }

        private JobUrgencyLevel GetSelectedUrgency()
        {
            if (IsJobUrgencyTrivialSelected)
            {
                return JobUrgencyLevel.Trivial;
            }
            else if (IsJobUrgencyLowSelected)
            {
                return JobUrgencyLevel.Low;
            }
            else if (IsJobUrgencyMediumSelected)
            {
                return JobUrgencyLevel.Medium;
            }
            else if (IsJobUrgencyHighSelected)
            {
                return JobUrgencyLevel.High;
            }
            else if (IsJobUrgencyCriticalSelected)
            {
                return JobUrgencyLevel.Critical;
            }
            else
            {
                throw new ArgumentException("No urgency level is selected!");
            }
        }

        private JobType GetSelectedType()
        {
            if (IsJobTypeReservationSelected)
            {
                return JobType.Reservation;
            }
            else if (IsJobTypeMaintenanceSelected)
            {
                return JobType.Maintenance;
            }
            else if (IsJobTypeOfficeSelected)
            {
                return JobType.Office;
            }
            else if (IsJobTypeOtherSelected)
            {
                return JobType.Other;
            }
            else
            {
                throw new ArgumentException("No job type is selected!");
            }
        }

        private async Task<Guid?> GetSelectedEmployeeId()
        {
            if (AssignedEmployeeUsername == null)
            {
                return null;
            }

            var user = await _userService.GetUser(AssignedEmployeeUsername);
            return user?.UniqueId;
        }

        private async Task SaveAndClose()
        {
            // If the current user is not logged in, show a message and return.
            var user = Globals.CurrentUser;
            if (user == null)
            {
                MessageBox.Show("Cannot create a new job when there isn't a user logged in!\nPlease return to the application and log in before retrying your operation!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new job.
            var id = await _jobService.NewJob(JobTitle, JobDescription, GetSelectedUrgency(), GetSelectedType(), user.UniqueId, await GetSelectedEmployeeId()) ?? throw new ArgumentException("Somehow, the id is null! Check JobService - maybe something's gone wrong with NewJob.");
            Messenger.Send(new JobChangedEvent(id));

            // Close the window.
            await Messenger.Send(CreateOrDestroySubWindowEvent.DestroyWindow(typeof(JobWindow), typeof(JobWindowNewJobViewModel)));
        }

        private async Task Cancel()
        {
            // Close the window.
            await Messenger.Send(CreateOrDestroySubWindowEvent.DestroyWindow(typeof(JobWindow), this.GetType()));
        }
    }
}
