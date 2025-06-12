using AutoMapper;
using Common.Helpers;
using DataAccess.Entities;
using Repositories.Implementations;
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
            var existingStock = await _stockRepo.GetBySymbolAsync(dto.Symbol);

            if (existingStock != null)
                throw new InvalidOperationException("A stock with the same symbol already exists.");

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

        public async Task<PagedResult<StockDto>> GetAllAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true)
        {
            var result = await _stockRepo.GetPagedAsync(page, pageSize, sortBy, ascending);
            return new PagedResult<StockDto>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
                TotalItems = result.TotalItems,
                Items = result.Items.Select(item => _mapper.Map<StockDto>(item)).ToList()
            };
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

        public async Task<StockDto?> GetBySymbolAsync(string symbol)
        {
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null) return null;

            return _mapper.Map<StockDto>(stock);
        }

    }
}
