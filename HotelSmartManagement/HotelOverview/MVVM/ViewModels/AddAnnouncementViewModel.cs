using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.Models;
using System.Collections.ObjectModel;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public class AddAnnouncementViewModel : AddEntityViewModel
    {
        public override string Name => nameof(ManageInventoryViewModel);

        private HotelOverviewService _hotelOverviewService;
        private AnnouncementCategory _announcementCategory;
        private ObservableCollection<string> _announcementCategories;

        // Public properties.
        public AnnouncementCategory Category { get => _announcementCategory; set => SetProperty(ref _announcementCategory, value); }
        public ObservableCollection<string> AnnouncementCategories { get => _announcementCategories; }
        // Commands
        public AsyncRelayCommand OnSaveClose_Clicked { get; }
        public AsyncRelayCommand OnCancel_Clicked { get; }

#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public AddAnnouncementViewModel(Globals globals, HotelOverviewService hotelOverviewService) : base(globals)
#pragma warning restore CS8618 // Reason: private fields are set through public properties.
        {
            _hotelOverviewService = hotelOverviewService;
            _announcementCategories = [.. Enum.GetNames(typeof(AnnouncementCategory))];
            ErrorMessage = string.Empty;

            OnSaveClose_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => OnSave()));
            OnCancel_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(HotelOverviewDashboardViewModel)), nameof(MainViewModel))));
        }
        async public override void OnSave()
        {
            if (ValidateFields())
            {
                _hotelOverviewService.NewAnnouncement(Title, Details, Category);
                await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(HotelOverviewDashboardViewModel)), nameof(MainViewModel)));
            }

        }
    }
}
