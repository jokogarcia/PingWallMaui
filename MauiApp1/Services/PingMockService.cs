using PingWall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;


namespace PingWall.Services
{
     public class PingMockService : IPingService
    {
        double successRate;
        double averageReturnTime;
        double returnTimeMaxDeviation;
        static int lastId=0;
        var rand = new Random();
        public async Task<PingResult> Ping(string hostname){
            switch (hostname){
                case "100_100":
                    successRate=1;
                    averageReturnTime=100;
                    returnTimeMaxDeviation=10;
                    break;
                    case "75_500":
                    successRate=.75; averageReturnTime=500;returnTimeMaxDeviation=100;break
                    default:
                    successRate=rand.NextDouble();
                    averageReturnTime=(double)rand.Next(1000);
                    returnTimeMaxDeviation=rand.NextDouble*100;

            }
            PingResult result = new();
            result.PingId=lastId++;
            result.RoundTripMilliseconds = averageReturnTime + (rand.NextDouble()-.5)*returnTimeMaxDeviation*2;
            result.IsErrorState = rand.NextDouble() > this.successRate;
            result.ErrorMessage = "Error is error";
            await Task.Delay(TimeSpan.FromMiliseconds(result.RoundTripMilliseconds));
            result.Received = DateTime.Now();
            return result;
        }
    }
}