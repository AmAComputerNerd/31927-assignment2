using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.ViewModels;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;
using System.Collections.ObjectModel;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.ViewModels
{
    public class ReservationAndRoomsDashboardViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(ReservationAndRoomsDashboardViewModel);

        // Private
        private Uri _imageUri;
        private ObservableCollection<Reservation> _globalReservations;

        // Public
        public Uri ImageUri { get => _imageUri; set => SetProperty(ref _imageUri, value); }
        public ObservableCollection<Reservation> GlobalReservations { get => _globalReservations; set => SetProperty(ref _globalReservations, value); }

        // Commands
        public RelayCommand<Reservation> OnReservation_Clicked { get; }
        public RelayCommand<string> OnRoomDetails_Clicked { get; }

        public ReservationAndRoomsDashboardViewModel(ReservationAndRoomsService service, Globals globals) : base(globals)
        {
            OnReservation_Clicked = new RelayCommand<Reservation>((reservation) =>
            {
                _ = reservation ?? throw new ArgumentException("Invalid reservation selection!");
                var reservationDetailsViewModel = new ReservationDetailsViewModel(globals, reservation);
                Messenger.Send(new ChangeViewEvent(reservationDetailsViewModel), nameof(MainViewModel));
            });

            OnRoomDetails_Clicked = new RelayCommand<string>((roomType) =>
            {
                var roomDetailsViewModel = new RoomDetailsViewModel(service, globals, roomType);
                Messenger.Send(new ChangeViewEvent(roomDetailsViewModel), nameof(MainViewModel));
            });
        }
    }
}
