using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Repositories;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Views;
using System.Windows;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class JobWindowViewJobViewModel : ViewModelWithMessenging
    {
        // Fields: all backing data sources for mutable properties.
        private JobRepository _jobRepository;
        private Job? _currentJob;
        private string _jobUrgencyName;
        private string _jobStatusName;
        private string _jobTitle;
        private string _jobDescription;
        private bool _isJobTypeReservation;
        private bool _isJobTypeMaintenance;
        private bool _isJobTypeOffice;
        private bool _isJobTypeOther;
        private string? _assignedEmployeeName;
        
        // Properties: can be edited by the form or the viewmodel.
        public Job? CurrentJob { get => _currentJob; set => SetProperty(ref _currentJob, value); }
        public string JobUrgencyName { get => _jobUrgencyName; set => SetProperty(ref _jobUrgencyName, value); }
        public string JobStatusName { get => _jobStatusName; set => SetProperty(ref _jobStatusName, value); }
        public string JobTitle { get => _jobTitle; set => SetProperty(ref _jobTitle, value); }
        public string JobDescription { get => _jobDescription; set => SetProperty(ref _jobDescription, value); }
        public bool IsJobTypeReservation { get => _isJobTypeReservation; set => SetProperty(ref _isJobTypeReservation, value); }
        public bool IsJobTypeMaintenance { get => _isJobTypeMaintenance; set => SetProperty(ref _isJobTypeMaintenance, value); }
        public bool IsJobTypeOffice { get => _isJobTypeOffice; set => SetProperty(ref _isJobTypeOffice, value); }
        public bool IsJobTypeOther { get => _isJobTypeOther; set => SetProperty(ref _isJobTypeOther, value); }
        public string? AssignedEmployeeName { get => _assignedEmployeeName; set => SetProperty(ref _assignedEmployeeName, value); }

        public override string Name => nameof(JobWindowViewJobViewModel);

#pragma warning disable CS8618 // Reason: Properties will populate all non-nullable fields.
        public JobWindowViewJobViewModel(JobRepository jobRepository, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: Properties will populate all non-nullable fields.
        {
            _jobRepository = jobRepository;
            RefreshBindings();
        }

        public override void Initialise(params object[] args)
        {
            if (args.Length != 1 || args[0] is not Job currentJob)
            {
                throw new ArgumentException($"{nameof(JobWindowViewJobViewModel)} requires one argument of type {nameof(Job)} to be passed to the Initialise method. If you are trying to open a window to create a job, you should use view {nameof(JobWindowNewJobView)}");
            }
            CurrentJob = currentJob;
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
            JobUrgencyName = CurrentJob?.UrgencyLevel.ToFriendlyString() ?? "DESIGNER";
            JobStatusName = CurrentJob?.Status.ToFriendlyString() ?? "DESIGNER";
            JobTitle = CurrentJob?.Title ?? "DESIGNER";
            JobDescription = CurrentJob?.Description ?? "DESIGNER";
            // Set job type booleans - these are used to select the correct radio buttons (bound by RadioButton.IsChecked).
            IsJobTypeReservation = CurrentJob?.TaskType == JobType.Reservation;
            IsJobTypeMaintenance = CurrentJob?.TaskType == JobType.Maintenance;
            IsJobTypeOffice = CurrentJob?.TaskType == JobType.Office;
            IsJobTypeOther = CurrentJob?.TaskType == JobType.Other;

            AssignedEmployeeName = CurrentJob?.AssignedTo?.Username;
        }
    }
}
