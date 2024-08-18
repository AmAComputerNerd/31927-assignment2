using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class RoomRepository : Repository<Room>
    {
        public RoomRepository(HotelDbContext context) : base(context)
        {
        }

        public override IQueryable<Room> AsQueryable()
        {
            return _dbSet;
        }
    }
}
