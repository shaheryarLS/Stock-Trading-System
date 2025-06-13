using Common.Helpers;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> DeleteUserAsync(string id);
        Task<PagedResult<UserDto>> GetAllAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true);
        Task<UserDto?> GetByUserNameAsync(string userName);
        Task<UserDto?> GetByIdAsync(string id);
        Task<bool> UpdateUserAsync(string id, UpdateUserDto dto);
    }
}
