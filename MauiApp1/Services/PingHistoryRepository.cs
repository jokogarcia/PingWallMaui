using PingWall.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Services
{
    public class PingHistoryRepository : IPingHistoryRepository
    {
        SQLiteAsyncConnection db;
        private bool isInitialized = false;
        DateTime nextCleanupTime;
        private async Task Init()
        {
            await DeleteOldRecords();
            if (isInitialized) return;
            
            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PingsDatabase.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<PingResult>();
            await DeleteOldRecords(DateTime.Now - TimeSpan.FromDays(2), db);
            isInitialized = true;
        }
        private async Task DeleteOldRecords()
        {
            if (!isInitialized || DateTime.Now < nextCleanupTime) return;
            await DeleteOldRecords(DateTime.Now - TimeSpan.FromDays(2), db);
            nextCleanupTime = DateTime.Now + TimeSpan.FromHours(12);
        }
       

        private static Task DeleteOldRecords(DateTime cutoffDate,SQLiteAsyncConnection _db)
        {
            return _db.Table<PingResult>().DeleteAsync(x => x.Received < cutoffDate);
        }
        public async Task<int> AddAsync(PingResult item)
        {
            await Init();
            return await db.InsertAsync(item);
            
        }

        public async Task DeleteAsync(int id)
        {
            await Init();
            await db.DeleteAsync<PingResult>(id);
        }

        public async Task<List<PingResult>> GetAll()
        {
            await Init();
            var query = db.Table<PingResult>().Where(s => true);

            return await query.ToListAsync();
        }

        public async Task<PingResult> GetAsync(int id)
        {
            await Init();
            return await db.GetAsync<PingResult>(id);
        }

        public async Task InsertOrUpdateAsync(PingResult item)
        {
            await Init();
            await db.InsertOrReplaceAsync(item);
        }

        public async Task Modify(PingResult item)
        {
            await Init();
            await db.UpdateAsync(item);
        }
        public async Task Query(Func<PingResult,bool> predicate)
        {
            await Init();
            var x = db.Table<PingResult>().Where(s => predicate(s));
        }
        public async Task<double> GetSuccessRate(int id, DateTime startTime, DateTime endTime)
        {
            await Init();
            var nTotal =  db.Table<PingResult>().Where(item => item.Id == id && item.Received > startTime && item.Received < endTime);
            var successes = nTotal.Where(item => item.IsErrorState == false);
            var nSuccesses = await successes.CountAsync();
            var nTotals = await nTotal.CountAsync();
            if (nTotals == 0)
            {
                return 100.0;
            }
            return 100.0 * nSuccesses / nTotals;
        }
    }
}
