using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Windows;

namespace HotelSmartManagement.Common.Events
{
    public class CreateOrDestroySubWindowEvent : AsyncRequestMessage<Window>
    {
        public bool ShouldCreate { get; }
        public Type Window { get; }
        public Type WindowViewModel { get; }
        public object[] ConstructorArguments { get; }

        protected CreateOrDestroySubWindowEvent(bool shouldCreate, Type window, Type windowViewModel, object[] constructorArgs)
        {
            ShouldCreate = shouldCreate;
            Window = window;
            WindowViewModel = windowViewModel;
            ConstructorArguments = constructorArgs;
        }

        public static CreateOrDestroySubWindowEvent CreateWindow(Type window, Type windowViewModel, params object[] constructorArgs)
        {
            return new CreateOrDestroySubWindowEvent(true, window, windowViewModel, constructorArgs);
        }

        public static CreateOrDestroySubWindowEvent DestroyWindow(Type window, Type windowViewModel)
        {
            return new CreateOrDestroySubWindowEvent(false, window, windowViewModel, []);
        }
    }
}
