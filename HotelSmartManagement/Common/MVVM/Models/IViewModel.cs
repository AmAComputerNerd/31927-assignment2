namespace HotelSmartManagement.Common.MVVM.Models
{
    public interface IViewModel
    {
        void Initialise(params object[] args);
        Globals Globals { get; }
    }
}
