﻿using PingWall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;


namespace PingWall.Services
{
    public class PingService : IPingService
    {
        private Ping pingSender;
        TaskCompletionSource<PingResult> pingTaskCompletionSource;

        public Task<PingResult> Ping(string hostname)
        {
            if(pingTaskCompletionSource is not null && !pingTaskCompletionSource.Task.IsCompleted)
            {
                return Task.FromResult(new PingResult { IsErrorState = true, ErrorMessage = "Previous ping not finished" });
            }
            pingSender = new Ping();
            pingSender.PingCompleted += PingCompleted;
            pingSender.SendAsync(hostname,12000);
            pingTaskCompletionSource = new TaskCompletionSource<PingResult>();
            return pingTaskCompletionSource.Task;
        }

        private void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            PingResult pingResult = new();
            if (e.Cancelled)
            {
                pingResult.IsErrorState = true;
                pingResult.ErrorMessage = "Ping cancelled";
                pingTaskCompletionSource.SetResult(pingResult);
                return;
            }
            if (e.Error != null)
            {
                pingResult.IsErrorState = true;
                pingResult.ErrorMessage = e.Error.Message;
                pingTaskCompletionSource.SetResult(pingResult);
                return;

            }
            
            if(e.Reply.Status == IPStatus.Success)
            {
                pingResult.IsErrorState = false;
                pingResult.ErrorMessage = String.Empty;
                pingResult.RoundTripMilliseconds = e.Reply.RoundtripTime;
                pingTaskCompletionSource.SetResult(pingResult);
                return;
            }
            pingResult.IsErrorState = true;
            pingResult.ErrorMessage = e.Reply.Status.ToString();
            pingResult.RoundTripMilliseconds = -1;
            pingTaskCompletionSource.SetResult(pingResult);
            return;
        
        }
    }
}
