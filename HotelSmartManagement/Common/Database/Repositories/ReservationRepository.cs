using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class ReservationRepository : Repository<Reservation>
    {
        public ReservationRepository(HotelDbContext context) : base(context)
        {
        }

        public override IQueryable<Reservation> AsQueryable()
        {
            return _dbSet;
        }
    }
}
