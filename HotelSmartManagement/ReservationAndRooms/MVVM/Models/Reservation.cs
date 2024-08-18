using HotelSmartManagement.Common.Database.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.Models
{
    public class Reservation : IDatabaseObject
    {
        public Guid UniqueId { get; set; }

        public string? Reference { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Requests { get; set; }

        [ForeignKey("UniqueID")]
        public Guest? Guest { get; set; }

        [ForeignKey("RoomID")]
        public Room? Room { get; set; }
    }
}
