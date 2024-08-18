using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public class ManageInventoryViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(ManageInventoryViewModel);

        private HotelOverviewService _hotelOverviewService;

        // Public properties.
        public ObservableCollection<InventoryItem> InventoryItems { get; set; }
        // Commands
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }
        public AsyncRelayCommand OnClose_Clicked { get; }

#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public ManageInventoryViewModel(Globals globals, HotelOverviewService hotelOverviewService) : base(globals)
#pragma warning restore CS8618 // Reason: private fields are set through public properties.
        {
            _hotelOverviewService = hotelOverviewService;
            InventoryItems = new ObservableCollection<InventoryItem>(_hotelOverviewService.GetAllInventory() ?? Array.Empty<InventoryItem>());

            IncreaseQuantityCommand = new RelayCommand<InventoryItem>(IncreaseQuantity);
            DecreaseQuantityCommand = new RelayCommand<InventoryItem>(DecreaseQuantity);

            OnClose_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => OnClose()));
        }
        async public void OnClose()
        {
            await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(HotelOverviewDashboardViewModel)), nameof(MainViewModel)));
        }
        private void IncreaseQuantity(InventoryItem item)
        {
            if (item != null)
            {
                item.Quantity++;
                _hotelOverviewService.UpdateInventoryItem(item);
            }
        }

        private void DecreaseQuantity(InventoryItem item)
        {
            if (item != null && item.Quantity > 0)
            {
                item.Quantity--;
                _hotelOverviewService.UpdateInventoryItem(item);
            }
        }
    }
}
