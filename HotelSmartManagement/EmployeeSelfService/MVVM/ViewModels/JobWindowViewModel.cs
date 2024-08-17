using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class JobWindowViewModel : ParentViewModel
    {
        public JobWindowViewModel(IServiceProvider serviceProvider, Globals globals) : base(serviceProvider, globals)
        {
            CurrentView = serviceProvider.GetService<JobWindowNewJobViewModel>();
        }

        public JobWindowViewModel(IServiceProvider serviceProvider, Globals globals, Job job) : base(serviceProvider, globals)
        {
            // We need to pass this Job into the TaskWindowViewTaskViewModel so that it can display the correct information.
            CurrentView = serviceProvider.GetService<JobWindowViewJobViewModel>();
        }

        public override string Name => nameof(JobWindowViewModel);
    }
}
