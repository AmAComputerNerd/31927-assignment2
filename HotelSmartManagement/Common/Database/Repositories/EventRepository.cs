using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.HotelOverview.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class EventRepository : Repository<Event>
    {
        public EventRepository(HotelDbContext context) : base(context)
        {
        }

        public override IQueryable<Event> AsQueryable()
        {
            return _dbSet;
        }
    }
}
