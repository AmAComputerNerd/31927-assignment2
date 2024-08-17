using HotelSmartManagement.Common.Events;

namespace HotelSmartManagement.Common.MVVM.Models
{
    public abstract class ParentViewModel : ViewModelWithMessenging
    {
        protected IServiceProvider ServiceProvider { get; }

        private object? _currentView;
        public object? CurrentView { get => _currentView; set => SetProperty(ref _currentView, value); }

        public ParentViewModel(IServiceProvider serviceProvider, Globals globals) : base(globals)
        {
            ServiceProvider = serviceProvider;
        }

        protected override void OnActivated()
        {
            Messenger.Register<ParentViewModel, ChangeViewEvent, string>(this, Name, (recipient, message) => recipient.OnChildViewChanged(message));
        }

        protected virtual void OnChildViewChanged(ChangeViewEvent @event)
        {
            if (CurrentView?.GetType() != @event.ViewModel)
            {
                var viewModel = ServiceProvider.GetService(@event.ViewModel) ?? throw new ArgumentException($"ChangeViewEvent message provided an argument of type {@event.ViewModel}, but this ViewModel wasn't registered in the ServiceProvider.");
                CurrentView = viewModel;
            }
        }
    }
}
