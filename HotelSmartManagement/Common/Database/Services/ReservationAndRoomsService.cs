using HotelSmartManagement.Common.Database.Repositories;
using HotelSmartManagement.Common.Helpers;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

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
        public void AddReservation(string reference, DateTime startDate, DateTime endDate, string requests, Guest guest, Room room)
        {
            Reservation reservation = new Reservation() { UniqueId = new Guid(), Reference = reference, StartDate = startDate, EndDate = endDate, Requests = requests, Guest = guest, Room = room };
            _reservationRepository.Add(reservation);
            _reservationRepository.Save();
        }

        public void AddRoom(RoomType roomType, int size, int capacity, string description, List<string> amenities, List<string> photos, string layout)
        {
            Room room = new Room() { UniqueId = new Guid(), Type = roomType, Size = size, Capacity = capacity, Description = description, Amenities = amenities, Photos = photos, Layout = layout };
            _roomRepository.Add(room);
            _roomRepository.Save();
        }

        public void AddGuest(string firstName, string lastName, string tier, DateTime creationDate, int stays)
        {
            Guest guest = new Guest() { UniqueId = new Guid(), FirstName = firstName, LastName = lastName, Tier = tier, CreationDate = creationDate, Stays = stays };
            _guestRepository.Add(guest);
            _guestRepository.Save();
        }
        #endregion

        #region Query Database
        public Guest GetGuest(Guid id)
        {
            return _guestRepository.GetBy(guest => guest.UniqueId == id) ?? throw new NullReferenceException("Guest was empty.");
        }

        public Guest GetGuest(string firstName, string lastName)
        {
            return _guestRepository.GetBy(guest => guest.FirstName == firstName && guest.LastName == lastName) ?? throw new NullReferenceException("Guest was empty.");
        }

        public Reservation GetReservation(Guid id)
        {
            return _reservationRepository.GetBy(reservation => reservation.UniqueId == id) ?? throw new NullReferenceException("Reservation was empty.");
        }

        public Reservation GetReservation(string reference)
        {
            return _reservationRepository.GetBy(reservation => reservation.Reference == reference) ?? throw new NullReferenceException("Reservation was empty.");
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAll();
        }

        public Room GetRoom(Guid id)
        {
            return _roomRepository.GetBy(room => room.UniqueId == id) ?? throw new NullReferenceException("Room was empty.");
        }

        public Room GetRoom(int type)
        {
            return _roomRepository.GetBy(room => (int)room.Type == type) ?? throw new NullReferenceException("Room was empty.");
        }
        #endregion

        #region Delete From Database
        public void DeleteAllReservations()
        {
            var allReservations = _reservationRepository.GetAll().ToList();
            _reservationRepository.DeleteRange(allReservations);
            _reservationRepository.Save();
        }

        public void DeleteAllRooms()
        {
            var allRooms = _roomRepository.GetAll().ToList();
            _roomRepository.DeleteRange(allRooms);
            _roomRepository.Save();
        }

        public void DeleteAllGuests()
        {
            var allGuests = _guestRepository.GetAll().ToList();
            _guestRepository.DeleteRange(allGuests);
            _guestRepository.Save();
        }

        public void RemoveReservation(Guid id)
        {
            var reservation = _reservationRepository.GetBy(reservation => reservation.UniqueId == id);
            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();
        }

        public void RemoveReservation(string reference)
        {
            var reservation = _reservationRepository.GetBy(reservation => reservation.Reference == reference);
            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();
        }
        #endregion

        public async static void ExportReservationAsPDF(Reservation reservation, string filePath)
        {
            using (var writer = new PdfWriter(filePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    document.Add(new Paragraph("YOUR RESERVATION").SetBold().SetFontSize(30).SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(2f));
                    Table table = new Table(UnitValue.PERCENT).UseAllAvailableWidth();
                    table.AddCell("Reference");
                    table.AddCell($"{reservation.Reference}");
                    table.AddCell("Guest");
                    table.AddCell($"{reservation.Guest?.FullName}");
                    table.AddCell("Start Date");
                    table.AddCell($"{reservation.StartDate:d}");
                    table.AddCell("End Date");
                    table.AddCell($"{reservation.EndDate:d}");
                    table.AddCell("Special Requests");
                    table.AddCell($"{reservation.Requests}");
                    document.Add(table);
                    document.Add(new Paragraph("For any further enquiries, contact hotel concierge. This PDF reservation was generated upon request by the Hotel Smart Management System.").SetMarginTop(2f).SetFontSize(8).SetItalic());
                }
            }

            await EmailHelper.SendReservationEmailASync(reservation, "john.ly-1@student.uts.edu.au", filePath);
        }
    }
}
