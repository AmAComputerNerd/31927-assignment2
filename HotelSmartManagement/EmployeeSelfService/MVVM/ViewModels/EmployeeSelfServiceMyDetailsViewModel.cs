using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class EmployeeSelfServiceMyDetailsViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(EmployeeSelfServiceMyDetailsViewModel);

        public EmployeeSelfServiceMyDetailsViewModel(Globals globals) : base(globals)
        {
        }
    }
}
