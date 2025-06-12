using AutoMapper;
using DataAccess.Entities;
using Repositories.Interfaces;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepo;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepo, IMapper mapper)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
        }

        public async Task<StockDto> CreateAsync(CreateStockDto dto)
        {
            var stock = _mapper.Map<Stock>(dto);
            await _stockRepo.AddAsync(stock);
            await _stockRepo.SaveChangesAsync();
            return _mapper.Map<StockDto>(stock);
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null) return false;

            _stockRepo.Delete(stock);
            await _stockRepo.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<StockDto>> GetAllAsync()
        {
            return Task.Run(async () => 
            {
                var stocks = await _stockRepo.GetPagedAsync();
                return stocks.Items.Select(s => _mapper.Map<StockDto>(s));
            });
        }

        public async Task<StockDto?> GetByIdAsync(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            return stock == null ? null : _mapper.Map<StockDto>(stock);
        }

        public async Task<bool> UpdateStockAsync(int id, UpdateStockDto dto)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null) return false;

            _mapper.Map(dto, stock);
            await _stockRepo.SaveChangesAsync();
            return true;
        }
    }
}
