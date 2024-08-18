using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Repositories;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Views;
using HotelSmartManagement.EmployeeSelfService.SubWindows;
using System.Collections.ObjectModel;
using System.Windows;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class JobWindowViewJobViewModel : ViewModelWithMessenging
    {
        // Fields: all backing data sources for mutable properties.
        private readonly UserService _userService;
        private readonly JobService _jobService;
        private Job? _currentJob;
        private Guid _jobId;
        private Visibility _playVisibility;
        private string _playText;
        private string _jobUrgencyName;
        private string _jobStatusName;
        private string _jobTitle;
        private string _jobDescription;
        private bool _isJobTypeReservation;
        private bool _isJobTypeMaintenance;
        private bool _isJobTypeOffice;
        private bool _isJobTypeOther;
        private ObservableCollection<string> _employeeUsernames;
        private string? _selectedEmployeeUsername;

        // Properties: can be edited by the form or the viewmodel.
        public Job? CurrentJob { get => _currentJob; set => SetProperty(ref _currentJob, value); }
        public Guid JobId { get => _jobId; set => SetProperty(ref _jobId, value); }
        public Visibility PlayVisibility { get => _playVisibility; set => SetProperty(ref _playVisibility, value); }
        public string PlayText { get => _playText; set => SetProperty(ref _playText, value); }
        public string JobUrgencyName { get => _jobUrgencyName; set => SetProperty(ref _jobUrgencyName, value); }
        public string JobStatusName { get => _jobStatusName; set => SetProperty(ref _jobStatusName, value); }
        public string JobTitle { get => _jobTitle; set => SetProperty(ref _jobTitle, value); }
        public string JobDescription { get => _jobDescription; set => SetProperty(ref _jobDescription, value); }
        public bool IsJobTypeReservation { get => _isJobTypeReservation; set => SetProperty(ref _isJobTypeReservation, value); }
        public bool IsJobTypeMaintenance { get => _isJobTypeMaintenance; set => SetProperty(ref _isJobTypeMaintenance, value); }
        public bool IsJobTypeOffice { get => _isJobTypeOffice; set => SetProperty(ref _isJobTypeOffice, value); }
        public bool IsJobTypeOther { get => _isJobTypeOther; set => SetProperty(ref _isJobTypeOther, value); }
        public ObservableCollection<string> EmployeeUsernames { get => _employeeUsernames; set => SetProperty(ref _employeeUsernames, value); }
        public string? SelectedEmployeeUsername { get => _selectedEmployeeUsername; set => SetProperty(ref _selectedEmployeeUsername, value); }

        public AsyncRelayCommand OnPlay_Clicked { get; }
        public AsyncRelayCommand OnCloseJob_Clicked { get; }
        public AsyncRelayCommand OnSaveJob_Clicked { get; }
        public AsyncRelayCommand OnCancelJob_Clicked { get; }

        public override string Name => nameof(JobWindowViewJobViewModel);

#pragma warning disable CS8618 // Reason: Properties will populate all non-nullable fields.
        public JobWindowViewJobViewModel(UserService userService, JobService jobService, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: Properties will populate all non-nullable fields.
        {
            _userService = userService;
            _jobService = jobService;

            OnPlay_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => ToggleJobPlaying()));
            OnCloseJob_Clicked = new AsyncRelayCommand(CloseJob);
            OnSaveJob_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => SaveJob()));
            OnCancelJob_Clicked = new AsyncRelayCommand(CancelJob);
        }

        public JobWindowViewJobViewModel(Job currentJob, UserService userService, JobService jobService, Globals globals) : this(userService, jobService, globals)
        {
            CurrentJob = currentJob;
            RefreshBindings();
        }

        private void ToggleJobPlaying()
        {
            if (CurrentJob?.Status == JobStatus.InProgress)
            {
                // Retrieve the job from the database. That way, we don't overwrite the changes with any unintentional ones from this form.
                var job = _jobService.GetJob(CurrentJob.UniqueId) ?? throw new ArgumentException("Job doesn't exist or was deleted.");
                job.Status = JobStatus.Assigned;
                _jobService.UpdateJob(job);
                // Send a message to update job displays.
                Messenger.Send(new JobChangedEvent(job.UniqueId));
                // Replicate the changes in the viewmodel.
                CurrentJob.Status = JobStatus.Assigned;
                PlayText = "Resume";
            }
            else if (CurrentJob?.Status == JobStatus.Assigned)
            {
                // Retrieve the job from the database. That way, we don't overwrite the changes with any unintentional ones from this form.
                var job = _jobService.GetJob(CurrentJob.UniqueId) ?? throw new ArgumentException("Job doesn't exist or was deleted.");
                job.Status = JobStatus.InProgress;
                // Since time logging is inefficient and isn't fully implemented, we'll just iterate the hours spent by 1.
                job.TimeLoggedWorking++;
                _jobService.UpdateJob(job);
                // Send a message to update job displays.
                Messenger.Send(new JobChangedEvent(CurrentJob.UniqueId));

                // Replicate the changes in the viewmodel.
                CurrentJob.TimeLoggedWorking++;
                CurrentJob.Status = JobStatus.InProgress;
                PlayText = "Pause";
            }
        }

        private async Task CloseJob()
        {
            // Check that a job with this ID still exists.
            if (_jobService.GetJob(CurrentJob?.UniqueId ?? throw new ArgumentException("User logged out at an inopportune time.")) == null)
            {
                // Show a message box to the user.
                MessageBox.Show("This job has been deleted. Please refresh the list to see the changes.", "Concurrency > Job Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Close the job.
            ApplyPropertiesToJob();
            CurrentJob.Status = JobStatus.Completed;
            CurrentJob.ClosedBy = Globals.CurrentUser;
            CurrentJob.ClosedAtUtc = DateTime.Now;
            _jobService.UpdateJob(CurrentJob);
            // Send a message to close this window and to update job displays.
            Messenger.Send(new JobChangedEvent(CurrentJob.UniqueId));
            await Messenger.Send(CreateOrDestroySubWindowEvent.DestroyWindow(typeof(JobWindow), typeof(JobWindowViewModel)));
        }

        private void SaveJob()
        {
            // Check that a job with this ID still exists.
            if (_jobService.GetJob(CurrentJob?.UniqueId ?? throw new ArgumentException("User logged out at an inopportune time.")) == null)
            {
                // Show a message box to the user.
                MessageBox.Show("This job has been deleted. Please refresh the list to see the changes.", "Concurrency > Job Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Save the job.
            ApplyPropertiesToJob();
            _jobService.UpdateJob(CurrentJob);
            // Send a message to update job displays.
            Messenger.Send(new JobChangedEvent(CurrentJob.UniqueId));
        }

        private async Task CancelJob()
        {
            // Check that a job with this ID still exists.
            if (_jobService.GetJob(CurrentJob?.UniqueId ?? throw new ArgumentException("User logged out at an inopportune time.")) == null)
            {
                // Show a message box to the user.
                MessageBox.Show("This job has been deleted. Please refresh the list to see the changes.", "Concurrency > Job Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Cancel the job.
            ApplyPropertiesToJob();
            CurrentJob.Status = JobStatus.Cancelled;
            CurrentJob.ClosedBy = Globals.CurrentUser;
            CurrentJob.ClosedAtUtc = DateTime.Now;
            _jobService.UpdateJob(CurrentJob);
            // Send a message to close this window and to update job displays.
            Messenger.Send(new JobChangedEvent(CurrentJob.UniqueId));
            await Messenger.Send(CreateOrDestroySubWindowEvent.DestroyWindow(typeof(JobWindow), typeof(JobWindowViewModel)));
        }

        private void ApplyPropertiesToJob()
        {
            try
            {
                var job = CurrentJob ?? throw new ArgumentException("Job existn't.");
                job.Title = JobTitle;
                job.Description = JobDescription;
                job.TaskType = GetSelectedType();
                if (SelectedEmployeeUsername != null)
                {
                    job.AssignedTo = _userService.GetUser(SelectedEmployeeUsername);
                }
                _jobService.UpdateJob(job);
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to save changes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private JobType GetSelectedType()
        {
            if (IsJobTypeReservation)
            {
                return JobType.Reservation;
            }
            else if (IsJobTypeMaintenance)
            {
                return JobType.Maintenance;
            }
            else if (IsJobTypeOffice)
            {
                return JobType.Office;
            }
            else if (IsJobTypeOther)
            {
                return JobType.Other;
            }
            else
            {
                throw new ArgumentException("No job type is selected!");
            }
        }

        public override void Initialise(params object[] args)
        {
            if (args.Length != 1 || args[0] is not Job currentJob)
            {
                throw new ArgumentException($"{nameof(JobWindowViewJobViewModel)} requires one argument of type {nameof(Job)} to be passed to the Initialise method. If you are trying to open a window to create a job, you should use view {nameof(JobWindowNewJobView)}");
            }
            CurrentJob = currentJob;
            RefreshBindings();
        }

        protected override void OnActivated()
        {
            Messenger.Register<JobWindowViewJobViewModel, UserChangedEvent>(this, (recipient, message) => recipient.UserChanged(message));
        }

        private void UserChanged(UserChangedEvent @event)
        {
            if (@event.IsChangeInCurrentUser)
            {
                if (Globals.CurrentUser != null)
                {
                    RefreshBindings();
                }
                else
                {
                    MessageBox.Show("You have been logged out. Please log in again to continue.\nYour changes will not apply until you log back in!", "Concurrency > User Logged Out", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void JobChanged(JobChangedEvent @event)
        {
            if (@event.JobId == CurrentJob?.UniqueId)
            {
                MessageBox.Show("This job has been updated while you were working. Please reload the job to see the changes.", "Concurrency > Job Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshBindings();
            }
        }

        private void RefreshBindings()
        {
            JobId = CurrentJob?.UniqueId ?? Guid.Empty;
            JobUrgencyName = CurrentJob?.UrgencyLevel.ToFriendlyString() ?? "DESIGNER";
            JobStatusName = CurrentJob?.Status.ToFriendlyString() ?? "DESIGNER";
            JobTitle = CurrentJob?.Title ?? "DESIGNER";
            JobDescription = CurrentJob?.Description ?? "DESIGNER";
            // Set job type booleans - these are used to select the correct radio buttons (bound by RadioButton.IsChecked).
            IsJobTypeReservation = CurrentJob?.TaskType == JobType.Reservation;
            IsJobTypeMaintenance = CurrentJob?.TaskType == JobType.Maintenance;
            IsJobTypeOffice = CurrentJob?.TaskType == JobType.Office;
            IsJobTypeOther = CurrentJob?.TaskType == JobType.Other;

            EmployeeUsernames = new ObservableCollection<string>(_userService.GetAllUsers().Select(user => user.Username));
            if (CurrentJob?.AssignedTo != null)
            {
                SelectedEmployeeUsername = CurrentJob.AssignedTo.Username;
            }
            PlayVisibility = CurrentJob?.Status != JobStatus.Completed && CurrentJob?.Status != JobStatus.Cancelled && CurrentJob?.AssignedTo?.UniqueId == Globals.CurrentUser?.UniqueId ? Visibility.Visible : Visibility.Collapsed;
            PlayText = CurrentJob?.Status == JobStatus.InProgress ? "Pause" : (CurrentJob?.TimeLoggedWorking > 0 ? "Resume" : "Play");
        }
    }
}
