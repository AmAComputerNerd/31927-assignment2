using CommunityToolkit.Mvvm.ComponentModel;

namespace HotelSmartManagement.Common.MVVM.Models
{
    public abstract class ViewModelBase : ObservableObject, IViewModel
    {
        public Globals Globals { get; }

        public ViewModelBase(Globals globals)
        {
            Globals = globals;
        }
    }
}
