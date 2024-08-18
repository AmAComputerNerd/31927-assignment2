using HotelSmartManagement.Common.Helpers;
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
            CurrentView = serviceProvider.GetViewModel<JobWindowViewJobViewModel>([job]);
            // We need to pass this Job into the TaskWindowViewTaskViewModel so that it can display the correct information.
            // And since we can't use DI for this, we can call the Initialise method on the IViewModel - this is a method designed to provide after-construction initialisation for volatile objects.
            var viewModel = CurrentView as IViewModel;
            viewModel?.Initialise(job);
        }

        public override string Name => nameof(JobWindowViewModel);
    }
}
