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

        public virtual void Initialise(params object[] args)
        {
            // Do nothing.
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
