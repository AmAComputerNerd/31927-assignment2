using HotelSmartManagement.Common.Database.Repositories;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Services
{
    public class UserService
    {
        private UserRepository _userRepository;
        private EmployeeDetailsRepository _employeeDetailsRepository;
        private LeaveRequestRepository _leaveRequestRepository;

        public UserService(UserRepository userRepository, EmployeeDetailsRepository employeeDetailsRepository, LeaveRequestRepository leaveRequestRepository)
        {
            // Repositories retrieved through DI.
            _userRepository = userRepository;
            _employeeDetailsRepository = employeeDetailsRepository;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public Guid? NewUser(string username, string password) => NewUser(username, password, "");
        public Guid? NewUser(string username, string password, string email)
        {
            if (_userRepository.ContainsAny(user => user.Username == username))
            {
                // We already have a user registered with that username, so we shouldn't add another user with this username.
                // The reason we don't use the Username as the key is that PKs shouldn't be a string.
                return null;
            }

            var newUser = new User { Username = username, Password = password, Email = email, IsVerified = false };
            _userRepository.Add(newUser);
            _userRepository.Save();

            return newUser.UniqueId;
        }
        public Guid? NewEmployeeDetails(Guid userId) => NewEmployeeDetails(userId, 0, 0, "Cleaner", EmployeeStatus.PartTime, 30, 25.93, 0);
        public Guid? NewEmployeeDetails(Guid userId, int bankAccountNo, int bankAccountBSB, string jobPosition, EmployeeStatus jobStatus, int hoursPerWeek, double jobPayPerHour, double leaveBalanceInHours)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                // There isn't a user with that ID.
                return null;
            }

            var newEmployeeDetails = new EmployeeDetails() { BankAccountNo = bankAccountNo, BankAccountBSB = bankAccountBSB, JobPosition = jobPosition, JobStatus = jobStatus, JobHoursPerWeek = hoursPerWeek, JobActualHoursThisWeek = 0, JobPayPerHour = jobPayPerHour, LeaveBalanceInHours = leaveBalanceInHours, UserId = userId };
            _employeeDetailsRepository.Add(newEmployeeDetails);
            _employeeDetailsRepository.Save();

            return newEmployeeDetails.UniqueId;
        }
        public Guid? NewLeaveRequest(Guid userId, DateTime startAt, DateTime endAt, string description)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                // The user doesn't have an EmployeeDetails object, so we can't create a LeaveRequest for them.
                // This might be because (hypothetically) the user is not an employee, or that the user ID passed in is invalid.
                return null;
            }
            var employeeDetails = user.EmployeeDetails;
            if (employeeDetails == null)
            {
                // The user doesn't have an EmployeeDetails object, so we can't create a LeaveRequest for them.
                // This might be because (hypothetically) the user is not an employee, or that the user ID passed in is invalid.
                return null;
            }

            var newLeaveRequest = new LeaveRequest { StartAt = startAt, EndAt = endAt, Description = description, EmployeeDetailsId = employeeDetails.UniqueId };
            _leaveRequestRepository.Add(newLeaveRequest);
            _leaveRequestRepository.Save();

            return newLeaveRequest.UniqueId;
        }
        public Guid? NewPreApprovedLeaveRequest(Guid userId, DateTime startAt, DateTime endAt, string description)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                // The user doesn't have an EmployeeDetails object, so we can't create a LeaveRequest for them.
                // This might be because (hypothetically) the user is not an employee, or that the user ID passed in is invalid.
                return null;
            }
            var employeeDetails = user.EmployeeDetails;
            if (employeeDetails == null)
            {
                // The user doesn't have an EmployeeDetails object, so we can't create a LeaveRequest for them.
                // This might be because (hypothetically) the user is not an employee, or that the user ID passed in is invalid.
                return null;
            }

            var newLeaveRequest = new LeaveRequest { StartAt = startAt, EndAt = endAt, Description = description, EmployeeDetailsId = employeeDetails.UniqueId, IsApproved = true };
            _leaveRequestRepository.Add(newLeaveRequest);
            _leaveRequestRepository.Save();

            return newLeaveRequest.UniqueId;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User? GetUser(string username)
        {
            return _userRepository.GetBy(user => user.Username == username);
        }
        public User? GetUser(Guid guid)
        {
            return _userRepository.GetById(guid);
        }

        public EmployeeDetails? GetEmployeeDetails(Guid employeeDetailsId)
        {
            return _employeeDetailsRepository.GetById(employeeDetailsId);
        }
        public EmployeeDetails? GetEmployeeDetailsFromUser(Guid userId)
        {
            return _employeeDetailsRepository.GetBy(details => details.User.UniqueId == userId);
        }
        public EmployeeDetails? GetEmployeeDetailsFromUser(string username)
        {
            return _employeeDetailsRepository.GetBy(details => details.User.Username == username);
        }

        public LeaveRequest? GetLeaveRequest(Guid leaveRequestId)
        {
            return _leaveRequestRepository.GetById(leaveRequestId);
        }
        public IEnumerable<LeaveRequest> GetLeaveRequestsForUser(Guid userId)
        {
            return _leaveRequestRepository.GetAll().Where(leaveRequest => leaveRequest.EmployeeDetails.User.UniqueId == userId);
        }
        public IEnumerable<LeaveRequest> GetLeaveRequestsForUser(string username)
        {
            return _leaveRequestRepository.GetAll().Where(leaveRequest => leaveRequest.EmployeeDetails.User.Username == username);
        }

        public void UpdateUser(User user)
        {
            if (_userRepository.Contains(user))
            {
                _userRepository.Update(user);
            }
            else
            {
                _userRepository.Add(user);
            }
            _userRepository.Save();
        }
        public void UpdateEmployeeDetails(EmployeeDetails employeeDetails)
        {
            if (_employeeDetailsRepository.Contains(employeeDetails))
            {
                _employeeDetailsRepository.Update(employeeDetails);
            }
            else
            {
                _employeeDetailsRepository.Add(employeeDetails);
            }
            _employeeDetailsRepository.Save();
        }
        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            if (_leaveRequestRepository.Contains(leaveRequest))
            {
                _leaveRequestRepository.Update(leaveRequest);
            }
            else
            {
                _leaveRequestRepository.Add(leaveRequest);
            }
            _leaveRequestRepository.Save();
        }
    
        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
            _userRepository.Save();
        }
        public void DeleteAllUsers()
        {
            var allUsers = _userRepository.GetAll().ToList();
            _userRepository.DeleteRange(allUsers);
            _userRepository.Save();
        }
        public void DeleteEmployeeDetails(EmployeeDetails employeeDetails)
        {
            _employeeDetailsRepository.Delete(employeeDetails);
            _employeeDetailsRepository.Save();
        }
        public void DeleteAllEmployeeDetails()
        {
            var allEmployeeDetails = _employeeDetailsRepository.GetAll().ToList();
            _employeeDetailsRepository.DeleteRange(allEmployeeDetails);
            _employeeDetailsRepository.Save();
        }
        public void DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            _leaveRequestRepository.Delete(leaveRequest);
            _leaveRequestRepository.Save();
        }
        public void DeleteAllLeaveRequests()
        {
            var allLeaveRequests = _leaveRequestRepository.GetAll().ToList();
            _leaveRequestRepository.DeleteRange(allLeaveRequests);
            _leaveRequestRepository.Save();
        }
    }
}
