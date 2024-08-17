using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public class HotelManagementDashboardViewModel : ViewModelWithMessenging
    {
        public HotelManagementDashboardViewModel(Globals globals) : base(globals)
        {
        }

        public override string Name => nameof(HotelManagementDashboardViewModel);
    }
}
