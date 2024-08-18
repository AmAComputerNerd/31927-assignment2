using CommunityToolkit.Mvvm.Messaging;
using HotelSmartManagement.Common.Events;
using HotelSmartManagement.Common.Helpers;
using HotelSmartManagement.Common.MVVM.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace HotelSmartManagement.Common.MVVM.ViewModels
{
    public class MainViewModel : ParentViewModel
    {
        public override string Name => nameof(MainViewModel);
        protected List<Window> CreatedWindows { get; set; }

        public MainViewModel(IServiceProvider serviceProvider, Globals globals) : base(serviceProvider, globals)
        {
            CurrentView = ServiceProvider.GetRequiredService<MenuViewModel>();
            CreatedWindows = new List<Window>();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Messenger.Register<MainViewModel, CreateOrDestroySubWindowEvent>(this, (recipient, message) =>
            {
                if (message.ShouldCreate)
                {
                    OnCreateSubWindow(message);
                }
                else
                {
                    OnDestroySubWindow(message);
                }
            });
        }

        protected void OnCreateSubWindow(CreateOrDestroySubWindowEvent @event)
        {
            var windowType = @event.Window;
            var window = ServiceProvider.GetService(windowType) as Window ?? throw new ArgumentException($"ServiceProvider did not contain a window with type {@event.Window}, or the type wasn't actually a Window's type.");
            var windowViewModelType = @event.WindowViewModel;
            var windowViewModel = ServiceProvider.GetViewModel(windowViewModelType, @event.ConstructorArguments) ?? throw new ArgumentException($"ServiceProvider did not contain a viewmodel with type {@event.WindowViewModel}.");

            window.Owner = Application.Current.MainWindow;
            window.DataContext = windowViewModel;
            CreatedWindows.Add(window);
            @event.Reply(window);
        }

        protected void OnDestroySubWindow(CreateOrDestroySubWindowEvent @event)
        {
            var window = CreatedWindows.FirstOrDefault(w => w.GetType() == @event.Window);
            if (window != null)
            {
                window.Close();
                CreatedWindows.Remove(window);
                @event.Reply(window);
            }
        }
    }
}
