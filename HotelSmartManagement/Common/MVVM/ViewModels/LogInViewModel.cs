using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace HotelSmartManagement.Common.MVVM.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserService _userService;
        private string _title;
        private string _username;
        private string _password;
        private string? _confirmPassword;
        private string? _email;
        private Visibility _confirmPasswordVisibility;
        private bool _isRegister;

        public string Title { get => _title; }
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string? ConfirmPassword { get => _confirmPassword; set => SetProperty(ref _confirmPassword, value); }
        public string? Email { get => _email; set => SetProperty(ref _email, value); }
        public Visibility ConfirmPasswordVisibility { get => _confirmPasswordVisibility; }
        public bool IsRegister 
        { 
            get => _isRegister; 
            set
            {
                SetProperty(ref _isRegister, value);
                SetProperty(ref _isRegister, value, nameof(IsLogIn)); // Also update IsLogIn.
                SetProperty(ref _title, value ? "Register" : "Log In", nameof(Title));
                ConfirmPassword = "";
                Email = "";
                SetProperty(ref _confirmPasswordVisibility, value ? Visibility.Visible : Visibility.Collapsed, nameof(ConfirmPasswordVisibility));
            }
        }
        public bool IsLogIn { get => !IsRegister; set => IsRegister = !value; }
        public RelayCommand<PasswordBox> OnPassword_Changed { get; }
        public RelayCommand<PasswordBox> OnConfirmPassword_Changed { get; }

        public AsyncRelayCommand OnLogInRegister_Clicked { get; }

#pragma warning disable CS8618 // Reason: all fields are set through properties.
        public LogInViewModel(IServiceProvider serviceProvider, UserService userService, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: all fields are set through properties.
        {
            _serviceProvider = serviceProvider;
            _userService = userService;
            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = null;
            IsRegister = false;
            // Set Password and ConfirmPassword to the PasswordBox.Password property.
            OnPassword_Changed = new RelayCommand<PasswordBox>(param => Password = param?.Password ?? throw new ArgumentException("Password wasn't passed into the OnPassword_Changed command correctly!"));
            OnConfirmPassword_Changed = new RelayCommand<PasswordBox>(param => ConfirmPassword = param?.Password ?? throw new ArgumentException("ConfirmPassword wasn't passed into the OnConfirmPassword_Changed command correctly!"));
            OnLogInRegister_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => 
            {
                if (IsRegister)
                {
                    Register();
                }
                else
                {
                    LogIn();
                }
            }));
        }

        private void LogIn()
        {
            // Validate input.
            var user = _userService.GetUser(Username);
            if (user == null)
            {
                // User not found.
                MessageBox.Show("User not found.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (user.Password != Password)
            {
                // Password incorrect.
                MessageBox.Show("Password incorrect.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // User found and password correct.
            Globals.CurrentUser = user;
            WeakReferenceMessenger.Default.Send(new UserChangedEvent(false));
            WeakReferenceMessenger.Default.Send(new ChangeViewEvent(typeof(MenuViewModel)), nameof(MainViewModel));
        }

        private void Register()
        {
            // Validate input.
            if (Password.Length < 3)
            {
                // Password too short.
                MessageBox.Show("Password must be at least 3 characters long.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Password != ConfirmPassword)
            {
                // Passwords do not match.
                MessageBox.Show("Passwords do not match.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Email == null || Email.Length < 3 || Email.Contains('@') == false)
            {
                // Invalid email.
                MessageBox.Show("Invalid email. You must have a valid email to receive a verification code in the next step!", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Passwords matched, try to create a new user.
            var id = _userService.NewUser(Username, Password, Email);
            if (id == null)
            {
                // User already exists with that username.
                MessageBox.Show("Username must be unique.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Create a matching user details object.
            var employeeDetailsId = _userService.NewEmployeeDetails((Guid)id) ?? throw new ArgumentException("Something went wrong - we can't generate an EmployeeDetails model!");
            // User created successfully.
            var user = _userService.GetUser((Guid)id);
            Globals.CurrentUser = user;
            WeakReferenceMessenger.Default.Send(new UserChangedEvent(false));

            // Change view to VerifyEmailViewModel.
            WeakReferenceMessenger.Default.Send(new ChangeViewEvent(typeof(VerifyEmailViewModel)), nameof(MainViewModel));
        }
    }
}
