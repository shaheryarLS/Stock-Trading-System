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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ApplicationUser>> GetPagedAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true)
        {
            var query = _context.Users.AsQueryable();

            query = sortBy switch
            {
                "UserName" => ascending ? query.OrderBy(s => s.UserName) : query.OrderByDescending(s => s.UserName),
                "Email" => ascending ? query.OrderBy(s => s.Email) : query.OrderByDescending(s => s.Email),
                _ => query.OrderBy(s => s.Id) // default sort
            };

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<ApplicationUser>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<ApplicationUser?> GetByIdAsync(object id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task AddAsync(ApplicationUser user) => await _context.Users.AddAsync(user);
        public void Update(ApplicationUser user) => _context.Users.Update(user);

        public void Delete(ApplicationUser user)
        {
            _context.Users.Remove(user);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<ApplicationUser?> GetByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
