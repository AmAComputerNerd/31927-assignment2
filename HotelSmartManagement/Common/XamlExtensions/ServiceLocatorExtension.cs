using System.Windows;
using System.Windows.Markup;

namespace HotelSmartManagement.Common.XamlExtensions
{
    public class ServiceLocatorExtension : MarkupExtension
    {
        public Type? ServiceType { get; set; }

        public ServiceLocatorExtension()
        {
        }

        public ServiceLocatorExtension(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ((App)Application.Current).ServiceProvider.GetService(ServiceType ?? throw new ArgumentNullException($"Didn't provide a value for ServiceType."))
                ?? throw new ArgumentException($"ServiceProvider does not contain a service using type {ServiceType}.");
        }
    }
}
