using CommunityToolkit.Mvvm.ComponentModel;
using HotelSmartManagement.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace HotelSmartManagement.Common.MVVM.ViewModels
{
    public class MainViewModel : ObservableRecipient
    {
        private IServiceProvider _serviceProvider;

        private object _currentView;
        public object CurrentView { get => _currentView; set => SetProperty(ref _currentView, value); }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainViewModel(IServiceProvider serviceProvider)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _serviceProvider = serviceProvider;

            IsActive = true;
            CurrentView = _serviceProvider.GetRequiredService<MenuViewModel>();
        }

        protected override void OnActivated()
        {
            Messenger.Register<MainViewModel, ChangeViewEvent, string>(this, nameof(MainViewModel), (receiver, message) => receiver.ChangeView(message));
        }

        private void ChangeView(ChangeViewEvent @event)
        {
            if (CurrentView?.GetType() != @event.ViewModel.GetType())
            {
                var viewModel = _serviceProvider.GetService(@event.ViewModel) ?? throw new ArgumentException($"ChangeViewEvent message provided an argument of type {@event.ViewModel}, but this ViewModel wasn't registered in the ServiceProvider.");
                CurrentView = viewModel;
            }
        }
    }
}
