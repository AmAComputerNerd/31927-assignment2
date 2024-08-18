using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.ViewModels;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.ViewModels
{
    public class ReservationAndRoomsDashboardViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(ReservationAndRoomsDashboardViewModel);

        // Private
        private ObservableCollection<Reservation> _globalReservations;
        private ReservationAndRoomsService _service;
        private IServiceProvider _serviceProvider;

        // Public
        public ObservableCollection<Reservation> GlobalReservations { get => _globalReservations; set => SetProperty(ref _globalReservations, value); }

        // Commands
        public RelayCommand<Reservation> OnReservation_Clicked { get; }
        public RelayCommand<string> OnRoomDetails_Clicked { get; }

        protected ReservationAndRoomsService ReservationAndRoomsService
        {
            get
            {
                return _serviceProvider.GetRequiredService<ReservationAndRoomsService>();
            }
        }

#pragma warning disable CS8618 // Reason: fields set through properties.
        public ReservationAndRoomsDashboardViewModel(IServiceProvider serviceProvider, ReservationAndRoomsService service, Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: fields set through properties.
        {
            _service = service;
            _serviceProvider = serviceProvider;

            OnReservation_Clicked = new RelayCommand<Reservation>((reservation) =>
            {
                _ = reservation ?? throw new ArgumentException("Invalid reservation selection!");
                var reservationDetailsViewModel = new ReservationDetailsViewModel(service, globals, reservation);
                Messenger.Send(new ChangeViewEvent(reservationDetailsViewModel), nameof(MainViewModel));
            });

            OnRoomDetails_Clicked = new RelayCommand<string>((roomType) =>
            {
                var roomDetailsViewModel = new RoomDetailsViewModel(service, globals, roomType);
                Messenger.Send(new ChangeViewEvent(roomDetailsViewModel), nameof(MainViewModel));
            });
        }

        //private async void SeedData()
        //{
        //    var service = _serviceProvider.GetRequiredService<ReservationAndRoomsService>();
        //    service.DeleteAllReservations();
        //    service.DeleteAllGuests();
        //    service.DeleteAllRooms();
        //    service.AddRoom(RoomType.Standard, 10, 10, new List<string> { "Bla" }, new List<string> { "Bla" }, "Bla");
        //    service.AddGuest("John", "Does", "Gold", DateTime.Now, 5);
        //    service.AddReservation("EEE", DateTime.Now, DateTime.Now, "None", service.GetGuest("John", "Does"), service.GetRoom(RoomType.Standard));
        //}
    }
}
