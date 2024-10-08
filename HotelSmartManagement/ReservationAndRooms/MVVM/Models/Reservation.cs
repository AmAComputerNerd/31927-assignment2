﻿using HotelSmartManagement.Common.Database.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.Models
{
    public class Reservation : IDatabaseObject
    {
        [Key]
        public Guid UniqueId { get; set; }

        public string? Reference { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Requests { get; set; }

        public Guid? GuestId { get; set; }

        [ForeignKey("GuestId")]
        public Guest? Guest { get; set; }

        public Guid? RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room? Room { get; set; }
    }
}
