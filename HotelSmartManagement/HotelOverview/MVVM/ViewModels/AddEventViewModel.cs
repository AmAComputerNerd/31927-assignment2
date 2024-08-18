using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.Models;
using System.Collections.ObjectModel;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public class AddEventViewModel : AddEntityViewModel
    {
        public override string Name => nameof(AddEventViewModel);

        private HotelOverviewService _hotelOverviewService;
        private Area _areaAffected;
        private ObservableCollection<Area> _areaTypes;

        // Public properties.
        public Area AreaAffected { get => _areaAffected; set => SetProperty(ref _areaAffected, value); }
        public ObservableCollection<Area> AreaTypes { get => _areaTypes; set => SetProperty(ref _areaTypes, value); }
        // Commands
        public AsyncRelayCommand OnSaveClose_Clicked { get; }
        public AsyncRelayCommand OnCancel_Clicked { get; }

#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public AddEventViewModel(Globals globals, HotelOverviewService hotelOverviewService) : base(globals)
#pragma warning restore CS8618 // Reason: private fields are set through public properties.
        {
            _hotelOverviewService = hotelOverviewService;

            OnSaveClose_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => OnSave()));
            OnCancel_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(HotelOverviewDashboardViewModel)), nameof(MainViewModel))));
            OnCancel_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(HotelOverviewDashboardViewModel)), nameof(MainViewModel))));
        }

        async public override void OnSave()
        {
            if (ValidateFields())
            {
                _hotelOverviewService.NewEvent(Title, Details, AreaAffected);
                await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(HotelOverviewDashboardViewModel)), nameof(MainViewModel)));
            }

        }
    }
}
