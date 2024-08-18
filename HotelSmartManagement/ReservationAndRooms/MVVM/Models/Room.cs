using HotelSmartManagement.Common.Database.Misc;
using System.ComponentModel.DataAnnotations;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.Models
{
    public class Room : IDatabaseObject
    {
        [Key]
        public Guid UniqueId { get; set; }

        public RoomType Type { get; set; }

        public int Size { get; set; }

        public int Capacity { get; set; }

        public string Description { get; set; }

        public List<string>? Amenities { get; set; }

        public List<string>? Photos { get; set; }

        public string? Layout { get; set; }
    }

    public enum RoomType
    {
        Standard,
        Deluxe,
        Suite,
        Collection
    }
}
