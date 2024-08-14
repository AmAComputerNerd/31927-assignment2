using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class EmployeeSelfServiceDashboardViewModel : ObservableRecipient
    {
        private Globals Globals { get; }
        private UserService _userService;

        private Uri _imageUri;
        public Uri ImageUri { get => _imageUri; set => SetProperty(ref _imageUri, value); }

#nullable disable // Reason: _imageUri is set when we set ImageUri.
        public EmployeeSelfServiceDashboardViewModel(Globals globals, UserService userService)
        {
#nullable enable // Reason: _imageUri is set when we set ImageUri.
            IsActive = true;

            Globals = globals;
            //ImageUri = Globals.GetProfilePictureUri();
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
        }
    }
}
