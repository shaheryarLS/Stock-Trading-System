using Common.Helpers;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ITradeRepository
    {
        Task<Trade?> GetByIdAsync(int tradeId);
        Task<PagedResult<Trade>> GetAllAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true);
        Task AddAsync(Trade trade);
        Task SaveChangesAsync();
    }
}
