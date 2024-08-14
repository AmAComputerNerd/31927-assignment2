using HotelSmartManagement.HotelOverview.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class InventoryItemRepository : Repository<InventoryItem>
    {
        public InventoryItemRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<InventoryItem> AsQueryable()
        {
            return _dbSet
                .Include(item => item.Name);
        }
    }
}
