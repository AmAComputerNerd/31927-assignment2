using HotelSmartManagement.Common.Database.Misc;

namespace HotelSmartManagement.HotelOverview.MVVM.Models
{
    public class InventoryItem : IDatabaseObject
    {
#nullable disable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        
#nullable enable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
    }
}
