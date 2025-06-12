using Common.Helpers;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class TradeRepository : ITradeRepository
    {
        private readonly ApplicationDBContext _context;

        public TradeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Trade trade)
        {
            await _context.Trades.AddAsync(trade);
        }

        public async Task<PagedResult<Trade>> GetAllAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true)
        {
            var query = _context.Trades.AsQueryable();

            query = sortBy switch
            {
                "TradeDate" => ascending ? query.OrderBy(t => t.TradeDate) : query.OrderByDescending(t => t.TradeDate),
                "StockSymbol" => ascending ? query.OrderBy(t => t.Stock.Symbol) : query.OrderByDescending(t => t.Stock.Symbol),
                _ => query.OrderBy(t => t.TradeId) // default sort
            };

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Trade>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<Trade?> GetByIdAsync(int tradeId)
        {
            return await _context.Trades
                .Include(t => t.User)
                .Include(t => t.Stock)
                .FirstOrDefaultAsync(t => t.TradeId == tradeId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
