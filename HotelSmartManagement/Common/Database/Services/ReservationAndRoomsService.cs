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

        public async void RemoveReservation(Guid id)
        {
            var reservation = await GetReservation(id);
            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();
        }

        public async void RemoveReservation(string reference)
        {
            var reservation = await GetReservation(reference);
            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();
        }

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
