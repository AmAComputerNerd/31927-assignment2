using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.ViewModels;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.ViewModels
{
    public class ReservationDetailsViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(RoomDetailsViewModel);

        // Private
        private Reservation _reservation;

        // Public
        public Reservation Reservation { get => _reservation; set => SetProperty(ref _reservation, value); }

        // Commands
        public RelayCommand OnDeleteReservation_Clicked { get; }
        public RelayCommand OnExportAsPDF_Clicked { get; }


        public ReservationDetailsViewModel(Globals globals, Reservation reservation) : base(globals)
        {
            _reservation = reservation;
            OnExportAsPDF_Clicked = new RelayCommand(() => ReservationAndRoomsService.ExportReservationAsPDF(reservation, ""));
            // OnDeleteReservation_Clicked
        }
    }
}
