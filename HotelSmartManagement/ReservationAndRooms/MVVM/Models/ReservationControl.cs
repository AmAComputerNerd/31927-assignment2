using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;
using HotelSmartManagement.Common.Database.Services;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.Common.MVVM.ViewModels;
using HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels;
using HotelSmartManagement.HotelOverview.MVVM.ViewModels;
using HotelSmartManagement.ReservationAndRooms.MVVM.Models;
using System.Collections.ObjectModel;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.Models
{
    public class ReservationControl : Control, INotifyPropertyChanged
    {
        private Reservation _reservation;

        public Reservation Reservation
        {
            get => _reservation;
            set
            {
                if (_reservation != value)
                {
                    _reservation = value;
                    OnPropertyChanged(nameof(Room));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static readonly DependencyProperty OnReservation_ClickedProperty =
        DependencyProperty.Register(nameof(OnReservation_Clicked), typeof(IRelayCommand), typeof(ReservationControl), new PropertyMetadata(null));

        public IRelayCommand OnReservation_Clicked
        {
            get => (IRelayCommand)GetValue(OnReservation_ClickedProperty);
            set => SetValue(OnReservation_ClickedProperty, value);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        static ReservationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ReservationControl), new FrameworkPropertyMetadata(typeof(ReservationControl)));
        }
    }
}
