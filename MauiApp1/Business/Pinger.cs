using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace PingWall.Business
{
    
    class Pinger
    {
        public string Hostname { get; set; }
        public delegate void OnPingResponse(long miliseconds);
        private OnPingResponse onPingResponse;
        private Action onPingTimeout;
        private Ping pingSender;
        private int timeoutms;
        private long DelayBetweenPings_ms = 3000;
        byte[] buffer;
        PingOptions options;
        Timer pingTimer;


        public Pinger(string hostname, OnPingResponse OnPingResponse, Action OnPingTimeout)
        {
            Hostname = hostname;
            onPingResponse = OnPingResponse;
            onPingTimeout = OnPingTimeout;
            pingSender = new();
            pingSender.PingCompleted += PingSender_PingCompleted;
            buffer = System.Text.Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"); //32 bytes of data
            timeoutms = 12000;
            options = new(64, true);
            pingTimer = new Timer(_timerCallback, null, 10, DelayBetweenPings_ms);


        }

        private void PingSender_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                throw new Exception("PING cancelled");
            }
            else if (e.Error != null)
            {
                onPingTimeout();

            }
            else
            {
                if (e.Reply.Status == IPStatus.TimedOut)
                {
                    onPingTimeout();
                }
                else
                    onPingResponse(e.Reply.RoundtripTime);
            }
            
            

        }

        private void _timerCallback(object state)
        {
            pingSender.SendAsync(Hostname, timeoutms, buffer, options);
        }
    }


}
