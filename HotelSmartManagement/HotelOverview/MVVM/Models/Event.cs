using HotelSmartManagement.Common.Database.Misc;
using System.ComponentModel.DataAnnotations;

namespace HotelSmartManagement.HotelOverview.MVVM.Models
{
    public class Event : IDatabaseObject
    {
#nullable disable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
        [Key]
        public Guid UniqueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Area AreaAffected { get; set; }
        public DateTime DateAffected { get; set; }
        
#nullable enable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
    }
}
