using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;

namespace HotelSmartManagement.Common.MVVM.Models
{
    public class Globals
    {
        public IConfiguration Configuration { get; }
        public User? CurrentUser { get; set; }

        public Globals(IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentUser = null;
        }
    }
}
