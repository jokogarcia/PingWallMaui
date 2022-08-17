using PingWall.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Services
{
    public interface IHostDTORepository
    {
        Task<List<HostDTO>> GetAll();
        Task<HostDTO> GetAsync(int id);
        Task<int> AddAsync (HostDTO item);
        Task UpdateAsync(HostDTO item);
        Task InsertOrUpdateAsync(HostDTO item);
        Task DeleteAsync(int id);

    }
}
