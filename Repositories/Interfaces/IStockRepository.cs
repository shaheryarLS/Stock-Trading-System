using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<Stock?> GetBySymbolAsync(string symbol);
    }
}
