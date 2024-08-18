using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.Models
{
    public class ReservationControl : Control
    {
        public static readonly DependencyProperty ReservationProperty =
            DependencyProperty.Register(
                "Reservation", 
                typeof(Reservation),
                typeof(ReservationControl), 
                new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty OnReservationClickedProperty =
            DependencyProperty.Register(
                "OnReservationClicked",
                typeof(RelayCommand<Reservation>),
                typeof(ReservationControl),
                new FrameworkPropertyMetadata(null));

        public Reservation Reservation
        {
            get => (Reservation)GetValue(ReservationProperty);
            set => SetValue(ReservationProperty, value);
        }

        public RelayCommand<Reservation> OnReservationClickedCommand
        {
            get => (RelayCommand<Reservation>)GetValue(OnReservationClickedProperty);
            set => SetValue(OnReservationClickedProperty, value);
        }

        static ReservationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ReservationControl), new FrameworkPropertyMetadata(typeof(ReservationControl)));
        }
    }
}
