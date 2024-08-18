using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.Helpers;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class EmployeeSelfServiceMyDetailsViewModel : ViewModelWithMessenging
    {
        private readonly UserService _userService;
        private string _fullName;
        private string _emailAddress;
        private string _password;
        private int _bankAccountNo;
        private int _bankAccountBsb;
        private string _jobPosition;
        private string _employeeStatus;
        private int _jobHoursPerWeek;
        private double _jobPayPerHour;
        private double _leaveBalanceInHours;
        private ObservableCollection<LeaveRequest> _userLeaveRequests;
        private LeaveRequest _selectedLeaveRequest;

        public string FullName { get => _fullName; set => SetProperty(ref _fullName, value); }
        public string EmailAddress { get => _emailAddress; set => SetProperty(ref _emailAddress, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public int BankAccountNo { get => _bankAccountNo; set => SetProperty(ref _bankAccountNo, value); }
        public int BankAccountBsb { get => _bankAccountBsb; set => SetProperty(ref _bankAccountBsb, value); }
        public string JobPosition { get => _jobPosition; set => SetProperty(ref _jobPosition, value); }
        public string EmployeeStatus { get => _employeeStatus; set => SetProperty(ref _employeeStatus, value); }
        public int JobHoursPerWeek { get => _jobHoursPerWeek; set => SetProperty(ref _jobHoursPerWeek, value); }
        public double JobPayPerHour { get => _jobPayPerHour; set => SetProperty(ref _jobPayPerHour, value); }
        public double LeaveBalanceInHours { get => _leaveBalanceInHours; set => SetProperty(ref _leaveBalanceInHours, value); }
        public ObservableCollection<LeaveRequest> UserLeaveRequests { get => _userLeaveRequests; set => SetProperty(ref _userLeaveRequests, value); }
        public LeaveRequest SelectedLeaveRequest { get => _selectedLeaveRequest; set => SetProperty(ref _selectedLeaveRequest, value); }

        public override string Name => nameof(EmployeeSelfServiceMyDetailsViewModel);

        public RelayCommand<PasswordBox> OnPassword_Changed { get; }

        public AsyncRelayCommand OnSave_Clicked { get; }
        public AsyncRelayCommand OnCancel_Clicked { get; }
        public AsyncRelayCommand OnAddRequest_Clicked { get; }
        public AsyncRelayCommand OnDeleteRequest_Clicked { get; }

#pragma warning disable CS8618 // Reason: all fields are set through RefreshBindings();
        public EmployeeSelfServiceMyDetailsViewModel(UserService userService, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: all fields are set through RefreshBindings();
        {
            _userService = userService;

            OnPassword_Changed = new RelayCommand<PasswordBox>(param => Password = param?.Password ?? throw new ArgumentException("Password wasn't passed into the OnPassword_Changed command correctly!"));

            OnSave_Clicked = new AsyncRelayCommand(async () => await Task.Run(() =>
            {
                var result = MessageBox.Show("Are you sure you want to save your changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SychroniseChanges();
                    Messenger.Send(new ChangeViewEvent(typeof(EmployeeSelfServiceDashboardViewModel)));
                }
            }));
            OnCancel_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(EmployeeSelfServiceDashboardViewModel)), nameof(MainViewModel))));
            OnAddRequest_Clicked = new AsyncRelayCommand(async () =>
            {
                var currentUser = Globals.CurrentUser;
                if (currentUser == null)
                {
                    // User is not logged in.
                    MessageBox.Show("You are not logged in. Please log in to add a leave request.", "Not logged in", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Guid? id;
                if (currentUser.Username == "admin")
                {
                    // Pre-approve leave, just for the demonstration.
                    id = _userService.NewPreApprovedLeaveRequest(currentUser.UniqueId, DateTime.MinValue, DateTime.MaxValue, "Pre-approved dummy leave request");
                }
                else
                {
                    // Create a new leave request.
                    id = _userService.NewLeaveRequest(currentUser.UniqueId, DateTime.MinValue, DateTime.MaxValue, "Dummy leave request");
                }

                if (id == null)
                {
                    MessageBox.Show("Failed to create a new leave request.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Creating your leave request confirmation.\n", "Creating Leave Request", MessageBoxButton.OK, MessageBoxImage.Information);
                var request = _userService.GetLeaveRequest((Guid)id) ?? throw new ArgumentException("Somehow, the leave request hasn't saved and hence we cannot get the leave request to send an email.");
                await EmailHelper.SendLeaveRequestCreationEmailAsync(currentUser.Email, request);

                Messenger.Send(new LeaveRequestChangedEvent((Guid)id));
            });
            OnDeleteRequest_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => 
            {
                var currentUser = Globals.CurrentUser;
                if (currentUser == null)
                {
                    // User is not logged in.
                    MessageBox.Show("You are not logged in. Please log in to delete a leave request.", "Not logged in", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (SelectedLeaveRequest == null)
                {
                    // No leave request selected.
                    MessageBox.Show("Please select a leave request to delete.", "No leave request selected", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                _userService.DeleteLeaveRequest(SelectedLeaveRequest);
                Messenger.Send(new LeaveRequestChangedEvent(SelectedLeaveRequest.UniqueId));
            }));
                
            RefreshBindings();
        }

        protected override void OnActivated()
        {
            Messenger.Register<EmployeeSelfServiceMyDetailsViewModel, UserChangedEvent>(this, (receiver, message) => receiver.UserChanged(message));
            Messenger.Register<EmployeeSelfServiceMyDetailsViewModel, LeaveRequestChangedEvent>(this, (receiver, message) => receiver.LeaveRequestChanged(message));
        }

        private void UserChanged(UserChangedEvent @event)
        {
            MessageBox.Show("User has changed or logged out. Please reload this page to continue editing!\nYour changes will not be apply unless you log back in!", "Concurrency > User Changed", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshBindings();
        }

        private void LeaveRequestChanged(LeaveRequestChangedEvent @event)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Retrieve the request from the database
                var requestFromDb = _userService.GetLeaveRequest(@event.LeaveRequestId);

                if (requestFromDb == null)
                {
                    // Leave request was deleted
                    // Attempt to find and remove the request from the user leave requests
                    var jobToRemove = UserLeaveRequests.FirstOrDefault(j => j.UniqueId == @event.LeaveRequestId);
                    if (jobToRemove != null)
                    {
                        UserLeaveRequests.Remove(jobToRemove);
                    }
                }
                else
                {
                    // Leave request exists in the database
                    var existingJob = UserLeaveRequests.FirstOrDefault(j => j.UniqueId == requestFromDb.UniqueId);

                    if (existingJob != null)
                    {
                        // Leave request is already in the assigned jobs list
                        if (requestFromDb.EmployeeDetails.User.UniqueId == Globals.CurrentUser?.UniqueId)
                        {
                            // Leave request owner hasn't changed, update the leave request
                            var index = UserLeaveRequests.IndexOf(existingJob);
                            UserLeaveRequests[index] = requestFromDb;
                        }
                        else
                        {
                            // Leave request owner has changed, remove the request
                            UserLeaveRequests.Remove(existingJob);
                        }
                    }
                    else if (requestFromDb.EmployeeDetails.User.UniqueId == Globals.CurrentUser?.UniqueId)
                    {
                        // Leave request is not in the list but is assigned to the current user, add it
                        UserLeaveRequests.Add(requestFromDb);
                    }
                }
            });
        }

        private void SychroniseChanges()
        {
            var currentUser = Globals.CurrentUser;
            if (currentUser == null)
            {
                // User is not logged in.
                MessageBox.Show("You are not logged in. Please log in to save your changes.", "Not logged in", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            currentUser.Username = FullName;
            currentUser.Email = EmailAddress;
            currentUser.Password = Password;
            currentUser.EmployeeDetails.BankAccountNo = BankAccountNo;
            currentUser.EmployeeDetails.BankAccountBSB = BankAccountBsb;
            currentUser.EmployeeDetails.JobPosition = JobPosition;
            currentUser.EmployeeDetails.JobHoursPerWeek = JobHoursPerWeek;
            currentUser.EmployeeDetails.JobPayPerHour = JobPayPerHour;
            currentUser.EmployeeDetails.LeaveBalanceInHours = LeaveBalanceInHours;
            _userService.UpdateUser(currentUser);
            Messenger.Send(new UserChangedEvent(false));
        }

        private void RefreshBindings()
        {
            FullName = Globals.CurrentUser?.Username ?? "Unknown";
            EmailAddress = Globals.CurrentUser?.Email ?? "Unknown";
            Password = Globals.CurrentUser?.Password ?? "Unknown";
            BankAccountNo = Globals.CurrentUser?.EmployeeDetails?.BankAccountNo ?? 0;
            BankAccountBsb = Globals.CurrentUser?.EmployeeDetails?.BankAccountBSB ?? 0;
            JobPosition = Globals.CurrentUser?.EmployeeDetails?.JobPosition ?? "Unknown";
            EmployeeStatus = Globals.CurrentUser?.EmployeeDetails?.JobStatus.ToFriendlyString(Globals.CurrentUser?.EmployeeDetails?.JobHoursPerWeek ?? 0) ?? "Unknown";
            JobHoursPerWeek = Globals.CurrentUser?.EmployeeDetails?.JobHoursPerWeek ?? 0;
            JobPayPerHour = Globals.CurrentUser?.EmployeeDetails?.JobPayPerHour ?? 0;
            LeaveBalanceInHours = Globals.CurrentUser?.EmployeeDetails?.LeaveBalanceInHours ?? 0;
            UserLeaveRequests = new ObservableCollection<LeaveRequest>();
            foreach (var leaveRequest in Globals.CurrentUser?.EmployeeDetails?.LeaveRequests ?? new List<LeaveRequest>())
            {
                UserLeaveRequests.Add(leaveRequest);
            }
        }
    }
}
