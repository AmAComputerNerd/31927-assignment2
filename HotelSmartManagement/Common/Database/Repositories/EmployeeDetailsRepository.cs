using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class EmployeeDetailsRepository : Repository<EmployeeDetails>
    {
        public EmployeeDetailsRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<EmployeeDetails> AsQueryable()
        {
            return _dbSet
                .Include(details => details.User)
                .Include(details => details.LeaveRequests);
        }
    }
}
