using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HotelSmartManagement.Common.XamlExtensions
{
    public static class UniformGridHelper
    {
        public static Thickness GetChildMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ChildMarginProperty);
        }

        public static void SetChildMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ChildMarginProperty, value);
        }

        public static Thickness GetChildPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ChildPaddingProperty);
        }

        public static void SetChildPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ChildPaddingProperty, value);
        }

        public static readonly DependencyProperty ChildMarginProperty =
            DependencyProperty.RegisterAttached("ChildMargin", typeof(Thickness), typeof(UniformGridHelper),
                new UIPropertyMetadata(new Thickness(), OnChildMarginChanged));

        public static readonly DependencyProperty ChildPaddingProperty = 
            DependencyProperty.RegisterAttached("ChildPadding", typeof(Thickness), typeof(UniformGridHelper),
                new UIPropertyMetadata(new Thickness(), OnChildPaddingChanged));

        private static void OnChildMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UniformGrid grid)
            {
                grid.Loaded += (sender, args) =>
                {
                    foreach (UIElement child in grid.Children)
                    {
                        if (child is FrameworkElement fe)
                        {
                            fe.Margin = (Thickness)e.NewValue;
                        }
                    }
                };
            }
        }

        private static void OnChildPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UniformGrid grid)
            {
                grid.Loaded += (sender, args) =>
                {
                    foreach (UIElement child in grid.Children)
                    {
                        if (child is Control control)
                        {
                            control.Padding = (Thickness)e.NewValue;
                        }
                    }
                };
            }
        }
    }
}
