using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.Helpers;
using HotelSmartManagement.Common.MVVM.Models;
using System.Windows;

namespace HotelSmartManagement.Common.MVVM.ViewModels
{
    public class VerifyEmailViewModel : ViewModelBase
    {
        private readonly UserService _userService;
        private int _verificationSentViaEmail;
        private string _email;
        private string _verificationCode;
        public string VerificationCode { get => _verificationCode; set => SetProperty(ref _verificationCode, value); }

        public AsyncRelayCommand OnResendEmail_Clicked { get; }
        public AsyncRelayCommand OnVerifyEmail_Clicked { get; }

#pragma warning disable CS8618 // Reason: We set all fields either through their properties or in SendVerificationEmail().
        public VerifyEmailViewModel(UserService userService, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: We set all fields either through their properties or in SendVerificationEmail().
        {
            // This ViewModel is used to verify the user's email address.
            // The user will receive an email with a number that they can enter into the application to verify their email address.
            _userService = userService;
            VerificationCode = "";
            OnResendEmail_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => SendVerificationEmail()));
            OnVerifyEmail_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => VerifyEmail()));

            _email = Globals.CurrentUser?.Email ?? throw new ArgumentException("VerifyEmailViewModel was called out of context - the user is not logged in!");
            SendVerificationEmail();
        }

        public void VerifyEmail()
        {
            if (int.TryParse(VerificationCode, out int verificationCode) && verificationCode == _verificationSentViaEmail)
            {
#pragma warning disable CS8602 // Reason: We know that Globals.CurrentUser is not null because we checked it in the constructor.
                Globals.CurrentUser.IsVerified = true;
#pragma warning restore CS8602 // Reason: We know that Globals.CurrentUser is not null because we checked it in the constructor.
                _userService.UpdateUser(Globals.CurrentUser);
                WeakReferenceMessenger.Default.Send(new UserChangedEvent(false));
                WeakReferenceMessenger.Default.Send(new ChangeViewEvent(typeof(MenuViewModel)), nameof(MainViewModel));
            }
            else
            {
                MessageBox.Show("The verification code you entered is incorrect. Please try again.", "Invalid verification code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void SendVerificationEmail()
        {
            var random = new Random();
            _verificationSentViaEmail = random.Next(100000, 999999);

            await EmailHelper.SendVerificationEmailAsync(_email, _verificationSentViaEmail);
        }
    }
}
