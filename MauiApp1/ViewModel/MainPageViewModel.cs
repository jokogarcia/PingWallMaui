using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Title = "Main Page";
        }
        [ObservableProperty]
        double _roundtripTime;

        [ObservableProperty]
        string _hostname;

        [ObservableProperty]
        double _successRate;

    }
}
