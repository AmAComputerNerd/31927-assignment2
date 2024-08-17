using HotelSmartManagement.Common.MVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.Models
{
    public class GuestDetails
    {
        [Key]
        public Guid UniqueId { get; set; }

        [InverseProperty("Guest")]
        public Reservation Reservation { get; set; }

        public Guid GuestId { get; set; }

        [ForeignKey(nameof(GuestId))]
        [InverseProperty(nameof(User.GuestDetails))]
        public User User { get; set; }
    }
}
