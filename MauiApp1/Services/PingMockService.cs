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
        Random rand = new Random();
        public async Task<PingResult> Ping(string hostname){
            switch (hostname){
                case "100_100":
                    successRate=1;
                    averageReturnTime=100;
                    returnTimeMaxDeviation=10;
                    break;
                    case "75_500":
                    successRate=.75; averageReturnTime=500;returnTimeMaxDeviation=100;break;
                    default:
                    successRate=rand.NextDouble();
                    averageReturnTime=rand.NextDouble()*1000;
                    returnTimeMaxDeviation=rand.NextDouble()*100;
                    break;

            }
            PingResult result = new();
            var thisRoundtrip = averageReturnTime + (rand.NextDouble() - .5) * returnTimeMaxDeviation * 2;
            thisRoundtrip = thisRoundtrip < 0 ? -thisRoundtrip : thisRoundtrip;
            thisRoundtrip = thisRoundtrip == 0 ? 7 : thisRoundtrip;
            result.RoundTripMilliseconds = (long)thisRoundtrip;
            result.IsErrorState = rand.NextDouble() > this.successRate;
            result.ErrorMessage = "Error is error";
            var roundTripSpan = TimeSpan.FromMilliseconds(thisRoundtrip);
            await Task.Delay(roundTripSpan);
            result.Received = DateTime.UtcNow;
            return result;
        }
    }
}