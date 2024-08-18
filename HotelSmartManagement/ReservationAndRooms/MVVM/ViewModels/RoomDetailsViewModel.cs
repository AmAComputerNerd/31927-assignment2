using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.HotelOverview.MVVM.ViewModels;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;
using HotelSmartManagement.Common.Database.Services;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.ViewModels
{
    public class RoomDetailsViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(RoomDetailsViewModel);

        // Private
        private Room _room;
        private ReservationAndRoomsService _reservationAndRoomsService;

        public Room Room { get => _room; set => SetProperty(ref _room, value); }

        public RoomDetailsViewModel(Globals globals, string roomType) : base(globals)
        {
            // _room = GetRoom(roomType)
        }
    }
}
