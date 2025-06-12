using Common.Helpers;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITradeService
    {
        Task<PagedResult<TradeDto>> GetAllAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true);
        Task<TradeDto> CreateAsync(CreateTradeDto dto);
    }
}
