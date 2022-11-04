using PingWall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Services
{
    public interface IPingService
    {
        Task<PingResult> Ping(string hostname, int timeout_ms);
    }
}
