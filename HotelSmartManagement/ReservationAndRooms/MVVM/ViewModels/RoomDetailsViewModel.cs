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
        private ReservationAndRoomsService _service;
        private ReservationAndRoomsService _reservationAndRoomsService;

        public Room Room { get => _room; set => SetProperty(ref _room, value); }

        public RoomDetailsViewModel(ReservationAndRoomsService service, Globals globals) : base(globals)
        {
            _service = service;
        }

        public RoomDetailsViewModel(ReservationAndRoomsService service, Globals globals, string roomType) : base(globals)
        {
            _service = service;
            SetRoom(roomType);
        }
        public async void SetRoom(string roomType)
        {
            _room = await _service.GetRoom((RoomType)int.Parse(roomType));
        }
    }
}
