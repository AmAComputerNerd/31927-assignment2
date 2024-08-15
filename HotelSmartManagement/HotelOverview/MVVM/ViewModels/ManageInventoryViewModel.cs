using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public class ManageInventoryViewModel : ViewModelBase
    {
        private Uri _imageUri;
        public Uri ImageUri { get => _imageUri; set => SetProperty(ref _imageUri, value); }

#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public ManageInventoryViewModel(Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: private fields are set through public properties.
        {
            //ImageUri = Globals.GetProfilePictureUri();
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
