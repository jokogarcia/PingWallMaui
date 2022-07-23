using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Model
{
    public class PingResult
    {
        public long RoundTripMilliseconds { get; set; }
        public bool IsErrorState { get; set; }
        public string ErrorMessage { get; set; }
    }
}
