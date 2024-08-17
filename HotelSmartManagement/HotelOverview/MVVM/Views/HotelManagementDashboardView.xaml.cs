using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelSmartManagement.HotelOverview.MVVM.Views
{
    /// <summary>
    /// Interaction logic for HotelManagementDashboardView.xaml
    /// </summary>
    public partial class HotelManagementDashboardView : UserControl
    {
        public HotelManagementDashboardView()
        {
            InitializeComponent();
            Loaded += HotelManagementDashboardView_Loaded;
        }
        private void HotelManagementDashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            TranslateTransform translateTransform = new TranslateTransform();
            MarqueeText.RenderTransform = translateTransform;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = this.ActualWidth,
                To = -MarqueeText.ActualWidth,
                Duration = new Duration(TimeSpan.FromSeconds(10)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);
        }
    }
}
