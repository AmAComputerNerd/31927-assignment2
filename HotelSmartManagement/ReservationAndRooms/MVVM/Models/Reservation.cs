using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.Models
{
    public class Reservation
    {
        [ForeignKey("GuestID")]
        public GuestDetails Guest { get; set; }

        [ForeignKey("RoomID")]
        public Room Room { get; set; }
    }
}
