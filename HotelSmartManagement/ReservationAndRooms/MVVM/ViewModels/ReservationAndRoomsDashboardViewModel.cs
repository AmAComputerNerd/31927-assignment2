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
        private Uri _imageUri;
        private ObservableCollection<Reservation> _globalReservations;
        private ReservationAndRoomsService _service;
        private IServiceProvider _serviceProvider;

        // Public
        public Uri ImageUri { get => _imageUri; set => SetProperty(ref _imageUri, value); }
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

        public ReservationAndRoomsDashboardViewModel(IServiceProvider serviceProvider, ReservationAndRoomsService service, Globals globals) : base(globals)
        {
            _service = service;
            _serviceProvider = serviceProvider;
            SeedData();

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

        private async void SeedData()
        {
            var service = _serviceProvider.GetRequiredService<ReservationAndRoomsService>();
            await service.DeleteAllReservations();
            await service.DeleteAllGuests();
            await service.DeleteAllRooms();
            await service.AddRoom(RoomType.Standard, 10, 10, new List<string> { "Bla" }, new List<string> { "Bla" }, "Bla");
            await service.AddGuest("John", "Does", "Gold", DateTime.Now, 5);
            await service.AddReservation("EEE", DateTime.Now, DateTime.Now, "None", await service.GetGuest("John", "Does"), await service.GetRoom(RoomType.Standard));
        }
    }
}
