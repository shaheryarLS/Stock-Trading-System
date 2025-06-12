using Common.Helpers;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<Stock> _stocks;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
            _stocks = context.Set<Stock>();
        }

        public async Task<PagedResult<Stock>> GetPagedAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true)
        {
            var query = _stocks.AsQueryable().Where(s => s.IsActive);

            query = sortBy switch
            {
                "Symbol" => ascending ? query.OrderBy(s => s.Symbol) : query.OrderByDescending(s => s.Symbol),
                "CompanyName" => ascending ? query.OrderBy(s => s.CompanyName) : query.OrderByDescending(s => s.CompanyName),
                _ => query.OrderBy(s => s.StockId) // default sort
            };

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Stock>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<Stock?> GetByIdAsync(object id)
        {
            var stock = await _stocks.FindAsync(id);
            return stock is { IsActive: true } ? stock : null;
        }

        public async Task AddAsync(Stock stock) => await _stocks.AddAsync(stock);
        public void Update(Stock stock) => _stocks.Update(stock);

        public void Delete(Stock stock)
        {
            stock.IsActive = false;
            _stocks.Update(stock);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
