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

        public Room Room { get => _room; set => SetProperty(ref _room, value); }

#pragma warning disable CS8618 // Reason: fields are set through public properties, or defined in Initialise().
        public RoomDetailsViewModel(ReservationAndRoomsService service, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: fields are set through public properties, or defined in Initialise().
        {
            _service = service;
        }

#pragma warning disable CS8618 // Reason: fields are set through public properties, or defined in Initialise().
        public RoomDetailsViewModel(ReservationAndRoomsService service, Globals globals, string roomType) : base(globals)
#pragma warning restore CS8618 // Reason: fields are set through public properties, or defined in Initialise().
        {
            _service = service;
            SetRoom(roomType);
        }
        public void SetRoom(string roomType)
        {
            _room = _service.GetRoom((RoomType)int.Parse(roomType));
        }
    }
}
