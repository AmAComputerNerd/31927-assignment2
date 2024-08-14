using HotelSmartManagement.HotelOverview.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class EventRepository : Repository<Event>
    {
        public EventRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<Event> AsQueryable()
        {
            return _dbSet
                .Include(Event => Event.Title);
        }
    }
}
