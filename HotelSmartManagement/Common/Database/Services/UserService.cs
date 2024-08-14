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

        public async Task<Guid?> NewUser(string username, string password) => await NewUser(username, password, "", "unknown-user.png");
        public async Task<Guid?> NewUser(string username, string password, string email) => await NewUser(username, password, email, "unknown-user.png");
        public async Task<Guid?> NewUser(string username, string password, string email, string profilePictureFileName)
        {
            if (await _userRepository.ContainsAny(user => user.Username == username))
            {
                // We already have a user registered with that username, so we shouldn't add another user with this username.
                // The reason we don't use the Username as the key is that PKs shouldn't be a string.
                return null;
            }

            var newUser = new User { Username = username, Password = password, Email = email, ProfilePictureFileName = profilePictureFileName };
            _userRepository.Add(newUser);
            _userRepository.Save();

            return newUser.UniqueId;
        }
    
        public async Task<User?> GetUser(string username)
        {
            return await _userRepository.GetBy(user => user.Username == username);
        }
        public async Task<User?> GetUser(Guid guid)
        {
            return await _userRepository.GetById(guid);
        }

        public async Task<EmployeeDetails?> GetEmployeeDetails(Guid employeeDetailsId)
        {
            return await _employeeDetailsRepository.GetById(employeeDetailsId);
        }
        public async Task<EmployeeDetails?> GetEmployeeDetailsFromUser(Guid userId)
        {
            return await _employeeDetailsRepository.GetBy(details => details.User.UniqueId == userId);
        }
        public async Task<EmployeeDetails?> GetEmployeeDetailsFromUser(string username)
        {
            return await _employeeDetailsRepository.GetBy(details => details.User.Username == username);
        }

        public IAsyncEnumerable<LeaveRequest> GetLeaveRequestsForUser(Guid userId)
        {
            return _leaveRequestRepository.GetAll().Where(leaveRequest => leaveRequest.EmployeeDetails.User.UniqueId == userId);
        }
        public IAsyncEnumerable<LeaveRequest> GetLeaveRequestsForUser(string username)
        {
            return _leaveRequestRepository.GetAll().Where(leaveRequest => leaveRequest.EmployeeDetails.User.Username == username);
        }

        public async void UpdateUser(User user)
        {
            if (!await _userRepository.Contains(user))
            {
                _userRepository.Update(user);
            }
            else
            {
                _userRepository.Add(user);
            }
            _userRepository.Save();
        }
        public async void UpdateEmployeeDetails(EmployeeDetails employeeDetails)
        {
            if (!await _employeeDetailsRepository.Contains(employeeDetails))
            {
                _employeeDetailsRepository.Update(employeeDetails);
            }
            else
            {
                _employeeDetailsRepository.Add(employeeDetails);
            }
            _employeeDetailsRepository.Save();
        }
        public async void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            if (!await _leaveRequestRepository.Contains(leaveRequest))
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
        public async void DeleteAllUsers()
        {
            var allUsers = await _userRepository.GetAll().ToListAsync();
            _userRepository.DeleteRange(allUsers);
            _userRepository.Save();
        }
        public void DeleteEmployeeDetails(EmployeeDetails employeeDetails)
        {
            _employeeDetailsRepository.Delete(employeeDetails);
            _employeeDetailsRepository.Save();
        }
        public async void DeleteAllEmployeeDetails()
        {
            var allEmployeeDetails = await _employeeDetailsRepository.GetAll().ToListAsync();
            _employeeDetailsRepository.DeleteRange(allEmployeeDetails);
            _employeeDetailsRepository.Save();
        }
        public void DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            _leaveRequestRepository.Delete(leaveRequest);
            _leaveRequestRepository.Save();
        }
        public async void DeleteAllLeaveRequests()
        {
            var allLeaveRequests = await _leaveRequestRepository.GetAll().ToListAsync();
            _leaveRequestRepository.DeleteRange(allLeaveRequests);
            _leaveRequestRepository.Save();
        }
    }
}
