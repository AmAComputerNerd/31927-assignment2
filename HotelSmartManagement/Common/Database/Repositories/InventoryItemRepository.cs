using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.HotelOverview.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class InventoryItemRepository : Repository<InventoryItem>
    {
        public InventoryItemRepository(HotelDbContext context) : base(context)
        {
        }

        public override IQueryable<InventoryItem> AsQueryable()
        {
            return _dbSet;
        }
    }
}
