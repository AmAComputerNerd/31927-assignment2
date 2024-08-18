using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.ViewModels
{
    public class ReservationDetailsViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(RoomDetailsViewModel);

        // Private
        private Reservation _reservation;
        private ReservationAndRoomsService _service;

        // Public
        public Reservation Reservation { get => _reservation; set => SetProperty(ref _reservation, value); }

        // Commands
        public RelayCommand OnExportAsPDF_Clicked { get; }

#pragma warning disable CS8618 // Reason: fields are set through properties.
        public ReservationDetailsViewModel(ReservationAndRoomsService service, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: fields are set through properties.
        {
            _service = service;
            OnExportAsPDF_Clicked = new RelayCommand(() => ReservationAndRoomsService.ExportReservationAsPDF(Reservation, "reservation.pdf"));
        }

        public override void Initialise(params object[] args)
        {
            if (args.Length != 1 || args[0] is not Reservation reservation)
            {
                throw new ArgumentException($"{nameof(RoomDetailsViewModel)} requires one argument of type string to be passed to the Initialise method.");
            }
            Reservation = reservation;
        }
    }
}
