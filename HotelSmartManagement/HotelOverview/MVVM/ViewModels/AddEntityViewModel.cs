using HotelSmartManagement.Common.MVVM.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HotelSmartManagement.HotelOverview.MVVM.ViewModels
{
    public abstract class AddEntityViewModel : ViewModelWithMessenging
    {
        public override string Name => nameof(AddEntityViewModel);

        private string _title;
        private string _details;

        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Details { get => _details; set => SetProperty(ref _details, value); }
        public string ErrorMessage { get; set; }
#pragma warning disable CS8618 // Reason: private fields are set through public properties.
        public AddEntityViewModel(Globals globals) : base(globals)
#pragma warning restore CS8618 // Reason: private fields are set through public properties.
        {
            ErrorMessage = string.Empty;
        }
        public abstract void OnSave();
        public virtual bool ValidateFields()
        {
            if (Title == string.Empty || Details == string.Empty)
            {
                ErrorMessage = "Please fill out all fields.";
                return false;
            }
            return true;
        }
    }
}
