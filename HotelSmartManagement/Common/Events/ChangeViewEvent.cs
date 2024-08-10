namespace HotelSmartManagement.Common.Events
{
    public class ChangeViewEvent
    {
        public Type ViewModel { get; private set; }

        public ChangeViewEvent(Type viewModelType)
        {
            ViewModel = viewModelType;
        }

        public ChangeViewEvent(object viewModel)
        {
            ViewModel = viewModel.GetType();
        }
    }
}
