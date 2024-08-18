using HotelSmartManagement.Common.Database.Repositories;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSmartManagement.Common.Database.Services
{
    public class ReservationAndRoomsService
    {
        private GuestRepository _guestRepository;
        private RoomRepository _roomRepository;
        private ReservationRepository _reservationRepository;

        public ReservationAndRoomsService(GuestRepository guestRepository, RoomRepository roomRepository, ReservationRepository reservationRepository)
        {
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
        }
        #region Add To Database
        public Task AddReservation(string reference, DateTime startDate, DateTime endDate, string requests, Guest guest, Room room)
        {
            return Task.Run(() =>
            {
                Reservation reservation = new Reservation() { UniqueId = new Guid(), Reference = reference, StartDate = startDate, EndDate = endDate, Requests = requests, Guest = guest, Room = room };
                _reservationRepository.Add(reservation);
                _reservationRepository.Save();
            });
        }

        public Task AddRoom(RoomType roomType, int size, int capacity, List<string> amenities, List<string> photos, string layout)
        {
            return Task.Run(() =>
            {
                Room room = new Room() { UniqueId = new Guid(), Type = roomType, Size = size, Capacity = capacity, Amenities = amenities, Photos = photos, Layout = layout };
                _roomRepository.Add(room);
                _roomRepository.Save();
            });
        }

        public Task AddGuest(string firstName, string lastName, string tier, DateTime creationDate, int stays)
        {
            return Task.Run(() =>
            {
                Guest guest = new Guest() { UniqueId = new Guid(), FirstName = firstName, LastName = lastName, Tier = tier, CreationDate = creationDate, Stays = stays };
                _guestRepository.Add(guest);
                _guestRepository.Save();
            });
        }
        #endregion

        #region Query Database
        public async Task<Guest> GetGuest(Guid id)
        {
            return await _guestRepository.GetBy(guest => guest.UniqueId == id) ?? throw new NullReferenceException("Guest was empty.");
        }

        public async Task<Guest> GetGuest(string firstName, string lastName)
        {
            return await _guestRepository.GetBy(guest => guest.FirstName == firstName && guest.LastName == lastName) ?? throw new NullReferenceException("Guest was empty.");
        }

        public async Task<Reservation> GetReservation(Guid id)
        {
            return await _reservationRepository.GetBy(reservation => reservation.UniqueId == id) ?? throw new NullReferenceException("Reservation was empty.");
        }

        public async Task<Reservation> GetReservation(string reference)
        {
            return await _reservationRepository.GetBy(reservation => reservation.Reference == reference) ?? throw new NullReferenceException("Reservation was empty.");
        }

        public async Task<Room> GetRoom(Guid id)
        {
            return await _roomRepository.GetBy(room => room.UniqueId == id) ?? throw new NullReferenceException("Room was empty.");
        }

        public async Task<Room> GetRoom(RoomType type)
        {
            return await _roomRepository.GetBy(room => room.Type == type) ?? throw new NullReferenceException("Room was empty.");
        }
        #endregion

        #region Delete From Database
        public async Task DeleteAllReservations()
        {
            IAsyncEnumerable<Reservation> reservations = _reservationRepository.GetAll();
            await foreach (Reservation reservation in reservations)
            {
                _reservationRepository.Delete(reservation);
            }
        }

        public async Task DeleteAllRooms()
        {
            IAsyncEnumerable<Room> rooms = _roomRepository.GetAll();
            await foreach (Room room in rooms)
            {
                _roomRepository.Delete(room);
            }
        }

        public async Task DeleteAllGuests()
        {
            IAsyncEnumerable<Guest> guests = _guestRepository.GetAll();
            await foreach (Guest guest in guests)
            {
                _guestRepository.Delete(guest);
            }
        }

        public async Task RemoveReservation(Guid id)
        {
            var reservation = await GetReservation(id);
            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();
        }

        public async Task RemoveReservation(string reference)
        {
            var reservation = await GetReservation(reference);
            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();
        }
        #endregion

        public static void ExportReservationAsPDF(Reservation reservation, string filePath)
        {
            using (var writer = new PdfWriter(filePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);
                    // Debating whether to make this actually look good and fulfill additional criteria or not.
                }
            }
        }
    }
}
