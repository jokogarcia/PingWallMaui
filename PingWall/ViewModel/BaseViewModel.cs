using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.ViewModel
{
    public partial class BaseViewModel:ObservableObject
    {
        [ObservableProperty]
        string _title;
        [ObservableProperty]
        bool _isBusy;
    }
}
