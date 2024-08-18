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
#pragma warning restore CS8618 // Reason: custom control, doesn't have a constructor

        public Reservation Reservation
        {
            get { return (Reservation)GetValue(ReservationProperty); }
            set { SetValue(ReservationProperty, value); }
        }

        static ReservationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ReservationControl), new FrameworkPropertyMetadata(typeof(ReservationControl)));
        }
    }
}
