using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.Common.Database.Repositories;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.Common.MVVM.Views;
using HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels;
using HotelSmartManagement.EmployeeSelfService.MVVM.Views;
using HotelSmartManagement.EmployeeSelfService.SubWindows;
using HotelSmartManagement.HotelOverview.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.Views;
using HotelSmartManagement.ReservationAndRooms.MVVM.ViewModels;
using HotelSmartManagement.ReservationAndRooms.MVVM.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace HotelSmartManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
#nullable disable // Reason: We set these properties in OnStartup() rather than the constructor, as this is a GUI class.
        public IConfiguration Configuration { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }
#nullable enable // Reason: We set these properties in OnStartup() rather than the constructor, as this is a GUI class.

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetConfiguration();

            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private void SetConfiguration()
        {
            // Set environment variable (you can set this based on actual environment)
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            // Set the base path to the project root
            var projectRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

            // Build configuration
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(projectRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            Configuration = configurationBuilder.Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register Models & Miscellaneous
            services.AddSingleton(Configuration);
            services.AddSingleton<Globals>();

            // Register ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<MenuViewModel>();

            services.AddTransient<HotelManagementDashboardViewModel>();
            services.AddTransient<AddAnnouncementViewModel>();
            services.AddTransient<AddEventViewModel>();
            services.AddTransient<ManageInventoryViewModel>();

            services.AddTransient<EmployeeSelfServiceDashboardViewModel>();
            services.AddTransient<EmployeeSelfServiceMyDetailsViewModel>();
            services.AddTransient<JobWindowViewModel>();
            services.AddTransient<JobWindowNewJobViewModel>();
            services.AddTransient<JobWindowViewJobViewModel>();

            services.AddTransient<ReservationAndRoomsDashboardViewModel>();

            // Register DbContext
            services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            // Register Repositories
            services.AddTransient<UserRepository>();
            services.AddTransient<EmployeeDetailsRepository>();
            services.AddTransient<LeaveRequestRepository>();

            services.AddTransient<EventRepository>();
            services.AddTransient<AnnouncementRepository>();
            services.AddTransient<InventoryItemRepository>();

            // Register Services
            services.AddTransient<UserService>();

            // Register Views
            services.AddTransient<MainWindow>();
            services.AddTransient<MenuView>();

            services.AddTransient<HotelManagementDashboardView>();
            services.AddTransient<AddAnnouncementView>();
            services.AddTransient<AddEventView>();
            services.AddTransient<ManageInventoryView>();

            services.AddTransient<EmployeeSelfServiceDashboardView>();
            services.AddTransient<EmployeeSelfServiceMyDetailsView>();
            services.AddTransient<JobWindow>();
            services.AddTransient<JobWindowNewJobView>();
            services.AddTransient<JobWindowViewJobView>();

            services.AddTransient<ReservationAndRoomsDashboardView>();
        }
    }

}
