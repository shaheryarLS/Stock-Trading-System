using AutoMapper;
using Common.Helpers;
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
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepo;
        private readonly IMapper _mapper;

        public TradeService(ITradeRepository tradeRepo, IMapper mapper)
        {
            _tradeRepo = tradeRepo;
            _mapper = mapper;
        }

        public async Task<TradeDto> CreateAsync(CreateTradeDto dto)
        {
            var trade = _mapper.Map<Trade>(dto);
            await _tradeRepo.AddAsync(trade);
            await _tradeRepo.SaveChangesAsync();

            var newTrade = await _tradeRepo.GetByIdAsync(trade.TradeId);

            return _mapper.Map<TradeDto>(newTrade);
        }

        public async Task<PagedResult<TradeDto>> GetAllAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true)
        {
            var result = await _tradeRepo.GetAllAsync(page, pageSize, sortBy, ascending);
            return new PagedResult<TradeDto>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
                TotalItems = result.TotalItems,
                Items = result.Items.Select(item => _mapper.Map<TradeDto>(item)).ToList()
            };
        }
    }
}
