using CommunityToolkit.Mvvm.Input;
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
        private Visibility _homeVisibility;
        private Visibility _logOutVisibility;

        public override string Name => nameof(MainViewModel);
        protected List<Window> CreatedWindows { get; set; }

        public Visibility HomeVisibility { get => _homeVisibility; set => SetProperty(ref _homeVisibility, value); }
        public Visibility LogOutVisibility { get => _logOutVisibility; set => SetProperty(ref _logOutVisibility, value); }

        public AsyncRelayCommand OnHome_Clicked { get; }
        public AsyncRelayCommand OnLogOut_Clicked { get; }

        public MainViewModel(IServiceProvider serviceProvider, Globals globals) : base(serviceProvider, globals)
        {
            CurrentView = ServiceProvider.GetRequiredService<LogInViewModel>();
            CreatedWindows = new List<Window>();
            HomeVisibility = Visibility.Collapsed;
            LogOutVisibility = Visibility.Collapsed;
            OnHome_Clicked = new AsyncRelayCommand(async () => await Task.Run(() =>
            {
                if (CurrentView.GetType() != typeof(LogInViewModel) && CurrentView.GetType() != typeof(VerifyEmailViewModel))
                {
                    Messenger.Send(new ChangeViewEvent(typeof(MenuViewModel)), nameof(MainViewModel));
                }
            }));
            OnLogOut_Clicked = new AsyncRelayCommand(async () => await Task.Run(() =>
            {
                if (CurrentView.GetType() != typeof(LogInViewModel) && CurrentView.GetType() != typeof(VerifyEmailViewModel))
                {
                    Globals.CurrentUser = null;
                    Messenger.Send(new ChangeViewEvent(typeof(LogInViewModel)), nameof(MainViewModel));
                }
            }));
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

        protected override void OnChildViewChanged(ChangeViewEvent @event)
        {
            base.OnChildViewChanged(@event);
            var shouldBeHidden = @event.ViewModel == typeof(LogInViewModel) || @event.ViewModel == typeof(VerifyEmailViewModel);
            HomeVisibility = shouldBeHidden ? Visibility.Collapsed : Visibility.Visible;
            LogOutVisibility = shouldBeHidden ? Visibility.Collapsed : Visibility.Visible;
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
