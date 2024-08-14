using CommunityToolkit.Mvvm.ComponentModel;
using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public class AddEventViewModel : ObservableRecipient
    {
        private Globals Globals { get; }

        private Uri _imageUri;
        public Uri ImageUri { get => _imageUri; set => SetProperty(ref _imageUri, value); }

#nullable disable // Reason: _imageUri is set when we set ImageUri.
        public AddEventViewModel(Globals globals)
        {
#nullable enable // Reason: _imageUri is set when we set ImageUri.
            IsActive = true;

            Globals = globals;
            //ImageUri = Globals.GetProfilePictureUri();
        }

        protected override void OnActivated()
        {
            throw new NotImplementedException();
        }
        public void OnCancel()
        {
            throw new NotImplementedException();
        }
        public void OnSave()
        {
            throw new NotImplementedException();
        }
        public void ValidateFields()
        {
            throw new NotImplementedException();
        }
    }
}
