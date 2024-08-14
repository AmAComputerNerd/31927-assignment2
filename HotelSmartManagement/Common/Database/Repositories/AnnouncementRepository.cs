using HotelSmartManagement.HotelOverview.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class AnnouncementRepository : Repository<Announcement>
    {
        public AnnouncementRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<Announcement> AsQueryable()
        {
            return _dbSet
                .Include(announcement => announcement.Title);
        }
    }
}
