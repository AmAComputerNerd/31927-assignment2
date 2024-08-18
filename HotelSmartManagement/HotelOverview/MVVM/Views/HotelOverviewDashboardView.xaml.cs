using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HotelSmartManagement.HotelOverview.MVVM.Views
{
    /// <summary>
    /// Interaction logic for HotelManagementDashboardView.xaml
    /// </summary>
    public partial class HotelOverviewDashboardView : UserControl
    {
        public HotelOverviewDashboardView()
        {
            InitializeComponent();
            Loaded += HotelManagementDashboardView_Loaded;
        }

        private void HotelManagementDashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            StartMarqueeAnimation();
        }

        private void StartMarqueeAnimation()
        {
            double textWidth = MarqueeText1.ActualWidth;
            double canvasWidth = this.ActualWidth;

            Canvas.SetLeft(MarqueeText1, canvasWidth);
            Canvas.SetLeft(MarqueeText2, canvasWidth + textWidth);

            TranslateTransform translateTransform1 = new TranslateTransform();
            TranslateTransform translateTransform2 = new TranslateTransform();

            MarqueeText1.RenderTransform = translateTransform1;
            MarqueeText2.RenderTransform = translateTransform2;

            DoubleAnimation animation1 = new DoubleAnimation
            {
                From = canvasWidth,
                To = -canvasWidth - textWidth,
                Duration = new Duration(TimeSpan.FromSeconds(textWidth/100)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            DoubleAnimation animation2 = new DoubleAnimation
            {
                From = canvasWidth + textWidth,
                To = -textWidth,
                Duration = new Duration(TimeSpan.FromSeconds(textWidth/100)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            translateTransform1.BeginAnimation(TranslateTransform.XProperty, animation1);
            translateTransform2.BeginAnimation(TranslateTransform.XProperty, animation2);
        }
    }
}
