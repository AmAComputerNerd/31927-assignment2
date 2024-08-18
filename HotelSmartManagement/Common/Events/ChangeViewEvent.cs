using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.Common.Events
{
    public class ChangeViewEvent
    {
        public Type ViewModel { get; private set; }
        public object[] ParamsToInitialise { get; private set; }

        public ChangeViewEvent(Type viewModelType, params object[] paramsToInitialise)
        {
            ViewModel = viewModelType;
            ParamsToInitialise = paramsToInitialise;
        }

        public ChangeViewEvent(IViewModel viewModel, params object[] paramsToInitialise)
        {
            ViewModel = viewModel.GetType();
            ParamsToInitialise = paramsToInitialise;
        }
    }
}
