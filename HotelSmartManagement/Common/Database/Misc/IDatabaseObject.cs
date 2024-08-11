using System.ComponentModel.DataAnnotations;

namespace HotelSmartManagement.Common.Database.Misc
{
    public interface IDatabaseObject
    {
        [Key]
        Guid UniqueId { get; }
    }
}
