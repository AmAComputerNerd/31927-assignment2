using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.Models;
using System.Collections.ObjectModel;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public class HotelOverviewDashboardViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(HotelOverviewDashboardViewModel);

        // Private fields.
        private HotelOverviewService _hotelOverviewService;
        private string _events;
        private ObservableCollection<InventoryItem> _inventoryItems;
        private ObservableCollection<Announcement> _announcements;

        // Public properties.
        public string Events { get => _events; set => SetProperty(ref _events, value); }
        public ObservableCollection<InventoryItem> InventoryItems { get => _inventoryItems; set => SetProperty(ref _inventoryItems, value); }
        public ObservableCollection<Announcement> Announcements { get => _announcements; set => SetProperty(ref _announcements, value); }

        // Commands.
        public AsyncRelayCommand OnManageInventoryButton_Clicked { get; }
        public AsyncRelayCommand OnAddEventButton_Clicked { get; }
        public AsyncRelayCommand OnAddAnnouncementButton_Clicked { get; }

#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public HotelOverviewDashboardViewModel(Globals globals, HotelOverviewService hotelOverviewService) : base(globals)
#pragma warning restore CS8618 // Reason: private fields are set through public properties.
        {
            _hotelOverviewService = hotelOverviewService;

            OnManageInventoryButton_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(ManageInventoryViewModel)), nameof(MainViewModel))));
            OnAddEventButton_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(AddEventViewModel)), nameof(MainViewModel))));
            OnAddAnnouncementButton_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(AddAnnouncementViewModel)), nameof(MainViewModel))));
            RefreshUserBindings();
        }

        private void RefreshUserBindings()
        {
            InventoryItems = new ObservableCollection<InventoryItem>(_hotelOverviewService.GetAllInventory() as ICollection<InventoryItem> ?? Array.Empty<InventoryItem>());
            Announcements = new ObservableCollection<Announcement>(_hotelOverviewService.GetAllAnnouncements().Where(a => a.IsResolved) as ICollection<Announcement> ?? Array.Empty<Announcement>());
            Events = string.Empty;
            var eventList = _hotelOverviewService.GetAllEvents().ToBlockingEnumerable().ToList();
            if (eventList.Count() > 0)
            {
                Events += eventList[0].Title + ": " + eventList[0].Description + " - affecting " + eventList[0].AreaAffected.ToFriendlyString();
                for (int i = 1; i < eventList.Count(); i++)
                {
                    Events += " | " + eventList[i].Title + ": " + eventList[i].Description + " - affecting " + eventList[i].AreaAffected.ToFriendlyString();
                }
            }
        }
    }
}
