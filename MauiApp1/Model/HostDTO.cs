using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Model
{
    public class HostDTO
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string Hostname { get; set; }
        public string DisplayName { get; set; }
        public int Interval_Miliseconds { get; set; }
    }
}
