using Common.Helpers;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStockService
    {
        Task<StockDto> CreateAsync(CreateStockDto dto);
        Task<bool> DeleteStockAsync(int id);
        Task<PagedResult<StockDto>> GetAllAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true);
        Task<StockDto?> GetBySymbolAsync(string symbol);
        Task<StockDto?> GetByIdAsync(int id);
        Task<bool> UpdateStockAsync(int id, UpdateStockDto dto);
    }
}
