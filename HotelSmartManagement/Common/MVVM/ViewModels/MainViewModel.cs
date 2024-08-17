using HotelSmartManagement.Common.MVVM.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HotelSmartManagement.Common.MVVM.ViewModels
{
    public class MainViewModel : ParentViewModel
    {
        public override string Name => nameof(MainViewModel);

        public MainViewModel(IServiceProvider serviceProvider, Globals globals) : base(serviceProvider, globals)
        {
            CurrentView = ServiceProvider.GetRequiredService<MenuViewModel>();
        }
    }
}
