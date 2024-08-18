using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.ViewModels;
using HotelSmartManagement.ReservationAndRooms.MVVM.ViewModels;

namespace HotelSmartManagement.Common.MVVM.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public AsyncRelayCommand EmployeeSelfService_Selected { get; }
        public AsyncRelayCommand HotelManagement_Selected { get; }
        public AsyncRelayCommand ReservationAndRooms_Selected { get; }
        public AsyncRelayCommand Logout_Clicked { get; }

        public MenuViewModel(Globals globals) : base(globals)
        {
            EmployeeSelfService_Selected = new AsyncRelayCommand(async () => await Task.Run(() =>
            {
                WeakReferenceMessenger.Default.Send(new ChangeViewEvent(typeof(EmployeeSelfServiceDashboardViewModel)), nameof(MainViewModel));
            }));
            HotelManagement_Selected = new AsyncRelayCommand(async () => await Task.Run(() =>
            {
                WeakReferenceMessenger.Default.Send(new ChangeViewEvent(typeof(HotelManagementDashboardViewModel)), nameof(MainViewModel));
            }));
            ReservationAndRooms_Selected = new AsyncRelayCommand(async () => await Task.Run(() =>
            {
                WeakReferenceMessenger.Default.Send(new ChangeViewEvent(typeof(ReservationAndRoomsDashboardViewModel)), nameof(MainViewModel));
            }));
            Logout_Clicked = new AsyncRelayCommand(async () => await Task.Run(() =>
            {
                // Do something to do with logging out.
            }));
        }
    }
}
