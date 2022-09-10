using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Services
{
    public interface IFullScreenService
    {
        void EnterFullScreenMode();
        void ExitFullScreenMode();
    }
}
