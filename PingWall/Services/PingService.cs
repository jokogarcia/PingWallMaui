using PingWall.Model;
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

        public Task<PingResult> Ping(string hostname, int timeout_ms)
        {
            if(pingTaskCompletionSource is not null && !pingTaskCompletionSource.Task.IsCompleted)
            {
                return Task.FromResult(new PingResult { IsErrorState = true, ErrorMessage = "Previous ping not finished" });
            }
            pingSender = new Ping();

            // When the PingCompleted event is raised,
            // the PingCompletedCallback method is called.
            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

           

            // Set options for transmission:
            // The data can go through 64 gateways or routers
            // before it is destroyed, and the data packet
            // cannot be fragmented.
            PingOptions options = new PingOptions(64, true);


            // Send the ping asynchronously.
            // Use the waiter as the user token.
            // When the callback completes, it can wake up this thread.
            pingSender.SendAsync(hostname, timeout_ms, buffer, options);

            // Prevent this example application from ending.
            // A real application should do something useful
            // when possible.
            pingTaskCompletionSource = new TaskCompletionSource<PingResult>();
            return pingTaskCompletionSource.Task;
        }
        private void SendResult(PingResult result)
        {
            pingTaskCompletionSource.SetResult(result);
        }


        private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            PingResult pingResult = new();
            pingResult.Received = DateTime.UtcNow;
            if (e.Cancelled)
            {
                pingResult.IsErrorState = true;
                pingResult.ErrorMessage = "Ping cancelled";
                SendResult(pingResult);
                return;
            }
            if (e.Error != null)
            {
                pingResult.IsErrorState = true;
                pingResult.ErrorMessage = GetInnerMostExceptionMessage(e.Error);
                SendResult(pingResult);
                return;

            }
            
            if(e.Reply.Status == IPStatus.Success)
            {
                pingResult.IsErrorState = false;
                pingResult.ErrorMessage = String.Empty;
                pingResult.RoundTripMilliseconds = e.Reply.RoundtripTime;
                SendResult(pingResult);
                return;
            }
            pingResult.IsErrorState = true;
            pingResult.ErrorMessage = e.Reply.Status.ToString();
            pingResult.RoundTripMilliseconds = -1;
            SendResult(pingResult);
            return;
        
        }
        string GetInnerMostExceptionMessage(Exception e)
        {
            Exception f = e;
            while (!string.IsNullOrEmpty(f.InnerException?.Message))
            {
                f = f.InnerException;
            }
            return f.Message;
        }
    }
}
