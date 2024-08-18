using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using System.Windows;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class EmployeeSelfServiceMyDetailsViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(EmployeeSelfServiceMyDetailsViewModel);

        public AsyncRelayCommand OnSave_Clicked { get; }
        public AsyncRelayCommand OnCancel_Clicked { get; }

        public EmployeeSelfServiceMyDetailsViewModel(Globals globals) : base(globals)
        {
            OnSave_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => MessageBox.Show("Not yet ;)", "Feature not implemented yet", MessageBoxButton.OK)));
            OnCancel_Clicked = new AsyncRelayCommand(async () => await Task.Run(() => Messenger.Send(new ChangeViewEvent(typeof(EmployeeSelfServiceDashboardViewModel)), nameof(MainViewModel))));
        }
    }
}
