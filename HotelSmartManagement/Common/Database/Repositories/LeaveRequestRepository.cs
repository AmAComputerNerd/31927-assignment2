using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class LeaveRequestRepository : Repository<LeaveRequest>
    {
        public LeaveRequestRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<LeaveRequest> AsQueryable()
        {
            return _dbSet
                .Include(leave => leave.EmployeeDetails)
                    .ThenInclude(details => details.User)
                        .ThenInclude(user => user.EmployeeDetails);
        }
    }
}
