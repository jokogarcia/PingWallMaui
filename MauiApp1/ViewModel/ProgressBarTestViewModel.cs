using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.ViewModel
{
    internal partial class ProgressBarTestViewModel:BaseViewModel
    {
        [ObservableProperty]
        double progress = 0d;
        private Timer timer;
        public ProgressBarTestViewModel()
        {
            timer = new Timer(ProgressUpdate,null,1000,2000);
        }

        private void ProgressUpdate(object state)
        {
            Progress+=0.03;        
        }
    }
}
