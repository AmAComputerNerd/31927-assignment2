using CommunityToolkit.Mvvm.ComponentModel;
using HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels;

namespace HotelSmartManagement.Common.MVVM.ViewModels
{
    public class SwitchingViewModel : ObservableObject
    {
        private object? _currentView;
        public object? CurrentView { get => _currentView; set => SetProperty(ref _currentView, value); }

        public SwitchingViewModel()
        {
            CurrentView = new ExampleViewModel();
        }
    }
}
