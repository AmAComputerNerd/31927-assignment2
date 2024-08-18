using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class JobRepository : Repository<Job>
    {
        public JobRepository(HotelDbContext context) : base(context)
        {
        }

        public override IQueryable<Job> AsQueryable()
        {
            return _dbSet
                .Include(x => x.CreatedBy)
                .Include(x => x.AssignedTo)
                .Include(x => x.ClosedBy);
        }
    }
}
