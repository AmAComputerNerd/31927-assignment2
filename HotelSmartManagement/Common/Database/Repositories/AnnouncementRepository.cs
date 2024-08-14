using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.HotelOverview.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class AnnouncementRepository : Repository<Announcement>
    {
        public AnnouncementRepository(HotelDbContext context) : base(context)
        {
        }

        public override IQueryable<Announcement> AsQueryable()
        {
            return _dbSet;
        }
    }
}
