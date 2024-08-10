using HotelSmartManagement.Common;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.Common.MVVM.Views;
using HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels;
using HotelSmartManagement.EmployeeSelfService.MVVM.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace HotelSmartManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<EmployeeSelfServiceDashboardViewModel>();

            // Register Services
            // TODO: Database services

            // Register Views
            services.AddTransient<MainWindow>();
            services.AddTransient<MenuView>();
            services.AddTransient<EmployeeSelfServiceDashboardView>();
        }
    }

}
