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
        private UserService _userService;
        private Uri _imageUri;
        private User? _currentUser;
        private EmployeeDetails? _userEmployeeDetails;
        private ObservableCollection<LeaveRequest> _userLeaveRequests;
        private double _userTimeRecording;

        // Public properties.
        public Uri ImageUri { get => _imageUri; set => SetProperty(ref _imageUri, value); }
        public User? CurrentUser { get => _currentUser; set => SetProperty(ref _currentUser, value); }
        public EmployeeDetails? UserEmployeeDetails { get => _userEmployeeDetails; set => SetProperty(ref _userEmployeeDetails, value); }
        public ObservableCollection<LeaveRequest> UserLeaveRequests { get => _userLeaveRequests; set => SetProperty(ref _userLeaveRequests, value); }
        public double UserTimeRecording { get => _userTimeRecording; set => SetProperty(ref _userTimeRecording, value); }

        // Commands.
        public AsyncRelayCommand OnMyProfile_Clicked { get; }
        public AsyncRelayCommand OnMyEmployment_Clicked { get; }
        public AsyncRelayCommand OnAnyTask_Clicked { get; }

#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public EmployeeSelfServiceDashboardViewModel(Globals globals, UserService userService) : base(globals)
#pragma warning restore CS8618 // Reason: private fields are set through public properties.
        {
            _userService = userService;

            //ImageUri = Globals.GetProfilePictureUri();
            OnMyProfile_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(EmployeeSelfServiceMyDetailsViewModel)), nameof(MainViewModel))));
            OnMyEmployment_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(EmployeeSelfServiceMyDetailsViewModel)), nameof(MainViewModel))));
            OnAnyTask_Clicked = new AsyncRelayCommand(async () =>
            {
                var someRandomJob = new Job()
                {
                    Title = "Some Random Job",
                    Description = "This is a test of the Jobs system.",
                    UrgencyLevel = JobUrgencyLevel.Critical,
                    Status = JobStatus.Assigned,
                    TaskType = JobType.Reservation,
                    CreatedBy = Globals.CurrentUser ?? throw new ArgumentNullException("CurrentUser was null while a menu requiring a log-in was open."),
                    CreatedById = Globals.CurrentUser.UniqueId,
                    CreatedAtUtc = DateTime.UtcNow,
                    AssignedTo = Globals.CurrentUser,
                    AssignedToId = Globals.CurrentUser.UniqueId,
                };
                Window window = await Messenger.Send(CreateOrDestroySubWindowEvent.CreateWindow(typeof(JobWindow), typeof(JobWindowViewModel), someRandomJob)).Response;
                window.Show();
            });
            RefreshUserBindings();
        }

        protected override void OnActivated()
        {
            Messenger.Register<EmployeeSelfServiceDashboardViewModel, UserChangedEvent>(this, (receiver, message) => receiver.UserChanged(message));
        }

        private void UserChanged(UserChangedEvent @event)
        {
            if (@event.IsLogout || @event.FieldChanged?.Name == nameof(User.ProfilePictureFileName))
            {
                ImageUri = Globals.GetProfilePictureUri();
            }
            Messenger.Send(CreateOrDestroySubWindowEvent.DestroyWindow(typeof(JobWindow), typeof(JobWindowViewModel)));
            RefreshUserBindings();
        }

        private void RefreshUserBindings()
        {
            SynchroniseCollections();
            CurrentUser = Globals.CurrentUser;
            UserEmployeeDetails = Globals.CurrentUser?.EmployeeDetails;
            UserLeaveRequests = new ObservableCollection<LeaveRequest>(Globals.CurrentUser?.EmployeeDetails?.LeaveRequests ?? Array.Empty<LeaveRequest>());
            UserTimeRecording = Math.Round((UserEmployeeDetails?.JobActualHoursThisWeek ?? 0) / (double)(UserEmployeeDetails?.JobHoursPerWeek ?? 1) * 100, 2);
        }

        private void SynchroniseCollections()
        {
            if (UserEmployeeDetails != null && UserLeaveRequests != null)
            {
                UserEmployeeDetails.LeaveRequests = UserLeaveRequests.ToList();
                _userService.UpdateEmployeeDetails(UserEmployeeDetails);
            }
        }
    }
}
