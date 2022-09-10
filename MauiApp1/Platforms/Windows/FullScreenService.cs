using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Services
{
    public class FullScreenService : IFullScreenService
    {
        public void EnterFullScreenMode()
        {
            Debug.WriteLine("Soy Windows");
        }

        public void ExitFullScreenMode()
        {
            Debug.WriteLine("Soy Windows. Exit");
        }
    }
}
