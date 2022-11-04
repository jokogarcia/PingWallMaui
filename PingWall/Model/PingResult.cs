using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Model
{
    public class PingResult
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        [Indexed]
        public int PingId { get; set; }
        public long RoundTripMilliseconds { get; set; }
        public bool IsErrorState { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Received { get; set; }
    }
}
