using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.HotelOverview.MVVM.Models;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class GuestRepository : Repository<Guest>
    {
        public GuestRepository(HotelDbContext context) : base(context)
        {
        }

        public override IQueryable<Guest> AsQueryable()
        {
            return _dbSet;
        }
    }
}
