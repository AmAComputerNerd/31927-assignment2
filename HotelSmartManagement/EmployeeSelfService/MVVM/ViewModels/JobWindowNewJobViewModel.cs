using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.SubWindows;
using System.Collections.ObjectModel;
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
        private string _selectedUrgencyLevel;
        private ObservableCollection<string> _urgencyLevels;
        private bool _isJobTypeReservationSelected;
        private bool _isJobTypeMaintenanceSelected;
        private bool _isJobTypeOfficeSelected;
        private bool _isJobTypeOtherSelected;
        private ObservableCollection<string> _employeeUsernames;
        private string? _selectedEmployeeUsername;

        public string JobTitle { get => _jobTitle; set => SetProperty(ref _jobTitle, value); }
        public string JobDescription { get => _jobDescription; set => SetProperty(ref _jobDescription, value); }
        public ObservableCollection<string> UrgencyLevels { get => _urgencyLevels; set => SetProperty(ref _urgencyLevels, value); }
        public string SelectedUrgencyLevel { get => _selectedUrgencyLevel; set => SetProperty(ref _selectedUrgencyLevel, value); }
        public bool IsJobTypeReservationSelected { get => _isJobTypeReservationSelected; set => SetProperty(ref _isJobTypeReservationSelected, value); }
        public bool IsJobTypeMaintenanceSelected { get => _isJobTypeMaintenanceSelected; set => SetProperty(ref _isJobTypeMaintenanceSelected, value); }
        public bool IsJobTypeOfficeSelected { get => _isJobTypeOfficeSelected; set => SetProperty(ref _isJobTypeOfficeSelected, value); }
        public bool IsJobTypeOtherSelected { get => _isJobTypeOtherSelected; set => SetProperty(ref _isJobTypeOtherSelected, value); }
        public ObservableCollection<string> EmployeeUsernames { get => _employeeUsernames; set => SetProperty(ref _employeeUsernames, value); }
        public string? SelectedEmployeeUsername { get => _selectedEmployeeUsername; set => SetProperty(ref _selectedEmployeeUsername, value); }

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
            // JobUrgency selection
            UrgencyLevels = [JobUrgencyLevel.Trivial.ToFriendlyString(), JobUrgencyLevel.Low.ToFriendlyString(), JobUrgencyLevel.Medium.ToFriendlyString(), JobUrgencyLevel.High.ToFriendlyString(), JobUrgencyLevel.Critical.ToFriendlyString()];
            // JobType radio buttons
            IsJobTypeReservationSelected = true;
            IsJobTypeMaintenanceSelected = false;
            IsJobTypeOfficeSelected = false;
            IsJobTypeOtherSelected = false;
            // EmployeeUsername selection
            EmployeeUsernames = new ObservableCollection<string>(_userService.GetAllUsers().Select(user => user.Username));
        }

        private JobUrgencyLevel GetSelectedUrgency()
        {
            if (SelectedUrgencyLevel == JobUrgencyLevel.Trivial.ToFriendlyString())
            {
                return JobUrgencyLevel.Trivial;
            }
            else if (SelectedUrgencyLevel == JobUrgencyLevel.Low.ToFriendlyString())
            {
                return JobUrgencyLevel.Low;
            }
            else if (SelectedUrgencyLevel == JobUrgencyLevel.Medium.ToFriendlyString())
            {
                return JobUrgencyLevel.Medium;
            }
            else if (SelectedUrgencyLevel == JobUrgencyLevel.High.ToFriendlyString())
            {
                return JobUrgencyLevel.High;
            }
            else if (SelectedUrgencyLevel == JobUrgencyLevel.Critical.ToFriendlyString())
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
            if (SelectedEmployeeUsername == null)
            {
                return null;
            }

            var user = await _userService.GetUser(SelectedEmployeeUsername);
            return user?.UniqueId;
        }

        private async Task SaveAndClose()
        {
            try
            {
                // If the current user is not logged in, show a message and return.
                var user = Globals.CurrentUser;
                if (user == null)
                {
                    MessageBox.Show("Cannot create a new job when there isn't a user logged in!\nPlease return to the application and log in before retrying your operation!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            // Create a new job.
            var id = _jobService.NewJob(JobTitle, JobDescription, GetSelectedUrgency(), GetSelectedType(), user.UniqueId, await GetSelectedEmployeeId()) ?? throw new ArgumentException("Somehow, the id is null! Check JobService - maybe something's gone wrong with NewJob.");
            Messenger.Send(new JobChangedEvent(id));

                // Close the window.
                await Messenger.Send(CreateOrDestroySubWindowEvent.DestroyWindow(typeof(JobWindow), typeof(JobWindowViewModel)));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task Cancel()
        {
            // Close the window.
            await Messenger.Send(CreateOrDestroySubWindowEvent.DestroyWindow(typeof(JobWindow), typeof(JobWindowViewModel)));
        }
    }
}
