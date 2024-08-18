using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public class AddEventViewModel : ViewModelBase
    {
        public override string Name => nameof(AddEventViewModel);

        private HotelOverviewService _hotelOverviewService;
        private Area _areaAffected;
        private ObservableCollection<string> _areaTypes;
        private DateTime _dateAffected;

        // Public properties.
        public Area AreaAffected { get => _areaAffected; set => SetProperty(ref _areaAffected, value); }
        public ObservableCollection<string> AreaTypes { get => _areaTypes; }
        public DateTime DateAffected { get => _dateAffected; set => SetProperty(ref _dateAffected, value); }
        // Commands
        public AsyncRelayCommand OnSaveClose_Clicked { get; }
        public AsyncRelayCommand OnCancel_Clicked { get; }

#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public AddEventViewModel(Globals globals) : base(globals)
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
