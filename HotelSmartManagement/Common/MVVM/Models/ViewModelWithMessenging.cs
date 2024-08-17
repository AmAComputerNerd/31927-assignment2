using CommunityToolkit.Mvvm.ComponentModel;

namespace HotelSmartManagement.Common.MVVM.Models
{
    public abstract class ViewModelWithMessenging : ObservableRecipient, IViewModel
    {
        public Globals Globals { get; }
        public abstract string Name { get; }

        public ViewModelWithMessenging(Globals globals)
        {
            IsActive = true;
            Globals = globals;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
