using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.ViewModels
{
    public class ReservationAndRoomsDashboardViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(ReservationAndRoomsDashboardViewModel);

        // Private
        private List<Reservation> _reservations;
        private ReservationAndRoomsService _service;

        // Public
        public List<Reservation> Reservations { get => _reservations; set => SetProperty(ref _reservations, value); }

        // Commands
        public RelayCommand<string> OnReservation_Clicked { get; }
        public RelayCommand<string> OnRoomDetails_Clicked { get; }

        public ReservationAndRoomsDashboardViewModel(ReservationAndRoomsService service, Globals globals) : base(globals)
        {
            _service = service;

            SeedData();
            Reservations = _service.GetAllReservations().ToList();

            OnReservation_Clicked = new RelayCommand<string>((reservation) =>
            {
                _ = _service.GetReservation(reservation) ?? throw new ArgumentException("Invalid reservation selection!");
                var reservationDetailsViewModel = new ReservationDetailsViewModel(service, globals);
                Messenger.Send(new ChangeViewEvent(reservationDetailsViewModel, _service.GetReservation(reservation)), nameof(MainViewModel));
            });

            OnRoomDetails_Clicked = new RelayCommand<string>((roomType) =>
            {
                var roomDetailsViewModel = new RoomDetailsViewModel(service, globals);
                Messenger.Send(new ChangeViewEvent(roomDetailsViewModel, roomType), nameof(MainViewModel));
            });
        }

        private void SeedData()
        {
            _service.DeleteAllReservations();
            _service.DeleteAllGuests();
            _service.DeleteAllRooms();

            _service.AddRoom(RoomType.Standard, 200, 2,
                "The Standard room is a cozy and functional space \ndesigned for basic comfort and convenience. Ideal for travelers \nseeking a no-frills stay, this room provides essential amenities \nand a comfortable bed for a restful night. The decor is simple \nyet welcoming, with a functional layout that includes a TV, \na desk for work or planning, and a private bathroom with basic \ntoiletries. It's perfect for those who need a comfortable, practical \nroom at a reasonable price.",
                new List<string> { "Basic furnishings", "Single or double bed", "TV with cable channels", "Desk and chair", "Complimentary Wi-Fi", "Private bathroom with shower", "Basic toiletries", "Coffee/tea maker" },
                new List<string> { string.Empty },
                string.Empty);

            _service.AddRoom(RoomType.Deluxe, 300, 2,
                "The Deluxe room offers an enhanced experience with added space \nand upgraded amenities. This room type features more luxurious \nfurnishings and a larger bed, providing extra comfort for relaxation. \nGuests can enjoy a wider selection of TV channels and high-speed Wi-Fi, \nalong with a more spacious bathroom equipped with both a shower and \nbathtub. Additional touches like a mini-fridge and high-quality \ncoffee/tea maker add to the convenience, making it an excellent choice \nfor travelers looking for a bit more comfort and style.",
                new List<string> { "Upgraded furnishings", "Larger bed or king-size bed", "Enhanced TV with premium channels", "Larger desk and ergonomic chair", "Complimentary Wi-Fi with higher speed", "Private bathroom with shower and bathtub", "High-quality toiletries", "Coffee/tea maker with a selection of pods or sachets", "Mini-fridge", "Iron and ironing board" },
                new List<string> { string.Empty },
                string.Empty);

            _service.AddRoom(RoomType.Suite, 400, 2,
                "The Suite is a spacious and elegantly designed room that provides \na blend of luxury and functionality. With a separate living area \nand a larger bedroom, this room offers an upscale experience ideal \nfor those who need more space or prefer a more refined environment. \nGuests can enjoy premium TV channels, high-speed Wi-Fi, and a luxurious \nbathroom with a separate shower and freestanding bathtub. The Suite \nalso includes additional amenities like a mini-bar, bathrobe, and \nslippers, ensuring a high level of comfort and convenience.",
                new List<string> { "Spacious living area", "Separate bedroom with a king-size or larger bed", "Flat-screen TV with premium channels and streaming capabilities", "Larger desk with office chair", "Complimentary high-speed Wi-Fi", "Private bathroom with separate shower and bathtub", "Luxury toiletries", "Coffee/tea maker with a variety of options", "Mini-bar", "Iron and ironing board", "Bathrobe and slippers", "In-room safe" },
                new List<string> { string.Empty },
                string.Empty);

            _service.AddRoom(RoomType.Collection, 600, 2,
                "The Collection room represents the pinnacle of luxury and exclusivity. \nWith expansive living and sleeping areas, this room type offers a \npremium experience with exceptional attention to detail. Guests are \ngreeted with high-end furnishings, multiple flat-screen TVs with premium \nchannels and streaming services, and a private bathroom featuring top-of-the-line amenities. The room includes a fully stocked mini-bar or \n kitchenette, and often a private balcony or terrace for added enjoyment. \nAccess to exclusive services or lounges may also be included, making \nit the perfect choice for those seeking the ultimate in comfort and sophistication.",
                new List<string> { "Luxurious furnishings and decor", "Expansive living and sleeping areas", "King-size or larger bed with premium bedding", "Multiple flat-screen TVs with premium channels and streaming services", "Large desk with executive chair", "Complimentary high-speed Wi-Fi and additional tech amenities", "Private bathroom with high-end shower and freestanding bathtub", "Premium toiletries", "Coffee/tea maker with a range of options", "Fully stocked mini-bar or kitchenette", "Iron and ironing board", "Bathrobe and slippers", "In-room safe", "Private balcony or terrace (in some cases)", "Access to exclusive services or lounges (if applicable)" },
                new List<string> { string.Empty },
                string.Empty);

            _service.AddGuest("Alice", "Johnson", "Gold", new DateTime(2022, 5, 12), 10);
            _service.AddGuest("Bob", "Smith", "Platinum", new DateTime(2021, 8, 25), 20);
            _service.AddGuest("Clara", "Williams", "Silver", new DateTime(2023, 1, 7), 3);
            _service.AddGuest("David", "Brown", "Bronze", new DateTime(2023, 4, 30), 7);
            _service.AddGuest("Eva", "Davis", "Gold", new DateTime(2020, 11, 15), 15);

            _service.AddReservation("YH7F9J", new DateTime(2024, 8, 5), new DateTime(2024, 8, 10), "Late check-in", _service.GetGuest("Alice", "Johnson"), _service.GetRoom(2));
            _service.AddReservation("MV2K4L", new DateTime(2024, 7, 20), new DateTime(2024, 7, 25), "Allergy-friendly room", _service.GetGuest("Bob", "Smith"), _service.GetRoom(1));
            _service.AddReservation("RP5Q6M", new DateTime(2024, 8, 15), new DateTime(2024, 8, 18), "Early check-in", _service.GetGuest("Clara", "Williams"), _service.GetRoom(0));
            _service.AddReservation("JX3H8N", new DateTime(2024, 8, 10), new DateTime(2024, 8, 12), "Request a view", _service.GetGuest("David", "Brown"), _service.GetRoom(3));
            _service.AddReservation("ZN4G2R", new DateTime(2024, 8, 25), new DateTime(2024, 8, 30), "Pet-friendly room", _service.GetGuest("Eva", "Davis"), _service.GetRoom(2));
        }
    }
}
