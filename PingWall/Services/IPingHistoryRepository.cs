using PingWall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Services
{
    public interface IPingHistoryRepository
    {
        Task<List<PingResult>> GetAll();
        Task<PingResult> GetAsync(int id);
        Task<int> AddAsync(PingResult item);
        Task Modify(PingResult item);
        Task InsertOrUpdateAsync(PingResult item);
        Task DeleteAsync(int id);

        Task Query(Func<PingResult, bool> predicate);

        Task<double> GetSuccessRate(int id, DateTime startTime, DateTime endTime);
    }
}
