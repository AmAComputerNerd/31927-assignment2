using HotelSmartManagement.Common.MVVM.Models;
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

        // Public
        public Room Room { get => _room; set => SetProperty(ref _room, value); }

#pragma warning disable CS8618 // Reason: fields are set through public properties, or defined in Initialise().
        public RoomDetailsViewModel(ReservationAndRoomsService service, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: fields are set through public properties, or defined in Initialise().
        {
            _service = service;
        }

        public override void Initialise(params object[] args)
        {
            if (args.Length != 1 || args[0] is not string roomType)
            {
                throw new ArgumentException($"{nameof(RoomDetailsViewModel)} requires one argument of type string to be passed to the Initialise method.");
            }
            Room = _service.GetRoom(int.Parse(roomType));
        }
    }
}
