using Microsoft.Extensions.Configuration;

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
