using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSmartManagement.EmployeeSelfService.MVVM.ViewModels
{
    public class ExampleViewModel : ObservableObject
    {
        private string? _helloWorld;
        public string? HelloWorld { get => _helloWorld; set => SetProperty(ref _helloWorld, value); }

        public ExampleViewModel()
        {
            HelloWorld = "Hello, World!";
        }
    }
}
