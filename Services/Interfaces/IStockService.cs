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
        Task<IEnumerable<StockDto>> GetAllAsync();
        Task<StockDto> CreateAsync(CreateStockDto dto);
    }
}
