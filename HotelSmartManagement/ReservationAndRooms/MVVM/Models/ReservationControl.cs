using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public event PropertyChangedEventHandler PropertyChanged;

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
