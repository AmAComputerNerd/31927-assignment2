using System.Globalization;
using System.Windows.Data;

namespace HotelSmartManagement.Common.XamlExtensions
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            // Convert the enum to its string representation
            return Enum.GetName(value.GetType(), value) ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
