using AutoMapper;
using DataAccess.Entities;
using Repositories.Interfaces;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly IGenericRepository<Stock> _stockRepo;
        private readonly IMapper _mapper;

        public StockService(IGenericRepository<Stock> stockRepo, IMapper mapper)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockDto>> GetAllAsync()
        {
            var stocks = await _stockRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<StockDto>>(stocks);
        }

        public async Task<StockDto> CreateAsync(CreateStockDto dto)
        {
            var stock = _mapper.Map<Stock>(dto);
            await _stockRepo.AddAsync(stock);
            await _stockRepo.SaveChangesAsync();
            return _mapper.Map<StockDto>(stock);
        }
    }
}
