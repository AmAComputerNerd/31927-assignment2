﻿using HotelSmartManagement.Common.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<User> AsQueryable()
        {
            return _dbSet
                .Include(user => user.EmployeeDetails)
                    .ThenInclude(details => details.LeaveRequests)
                        .ThenInclude(leaveRequest => leaveRequest.EmployeeDetails);
        }
    }
}
