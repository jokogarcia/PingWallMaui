using PingWall.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Services
{
    public class HostDTORepository : IHostDTORepository
    {
        SQLiteAsyncConnection db;
        private bool isInitialized = false;
        public HostDTORepository()
        {
            
        }
        async Task Init()
        {
            if (isInitialized)
                return;
            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PingsDatabase.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<HostDTO>();
            isInitialized = true;

        }
        Task CheckIsInitialized()
        {
            if (!isInitialized)
            {
                return Init();
            }
            return Task.CompletedTask;
        }
 
        public async Task<int> AddAsync(HostDTO item)
        {
            await CheckIsInitialized();
            return await db.InsertAsync(item);
        }

        public async Task<List<HostDTO>> GetAll()
        {
            await CheckIsInitialized();
            var query = db.Table<HostDTO>().Where(s => true);

            return await query.ToListAsync();

        }

        public async Task<HostDTO> GetAsync(int id)
        {
            await CheckIsInitialized();
            return await db.GetAsync<HostDTO>(id);
        }

        public async Task UpdateAsync(HostDTO item)
        {
            await CheckIsInitialized();
            await db.UpdateAsync(item);
        }

        public async Task InsertOrUpdateAsync(HostDTO item)
        {
            await CheckIsInitialized();
            await db.InsertOrReplaceAsync(item);
        }
        public async Task DeleteAsync(int id)
        {
            await CheckIsInitialized();
            await db.DeleteAsync<HostDTO>(id);
        }
    }
}
