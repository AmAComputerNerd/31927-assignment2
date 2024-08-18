using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.Common.Events
{
    public class ChangeViewEvent
    {
        public Type ViewModel { get; private set; }

        public ChangeViewEvent(Type viewModelType)
        {
            ViewModel = viewModelType;
        }

        public ChangeViewEvent(IViewModel viewModel)
        {
            ViewModel = viewModel.GetType();
        }
    }
}
