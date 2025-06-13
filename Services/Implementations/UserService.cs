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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            _userRepo.Delete(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<UserDto>> GetAllAsync(int page = 1, int pageSize = 10, string? sortBy = null, bool ascending = true)
        {
            var result = await _userRepo.GetPagedAsync(page, pageSize, sortBy, ascending);
            return new PagedResult<UserDto>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
                TotalItems = result.TotalItems,
                Items = result.Items.Select(item => _mapper.Map<UserDto>(item)).ToList()
            };
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UpdateUserAsync(string id, UpdateUserDto dto)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            _mapper.Map(dto, user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto?> GetByUserNameAsync(string userName)
        {
            var user = await _userRepo.GetByUserNameAsync(userName);
            if (user == null) return null;

            return _mapper.Map<UserDto>(user);
        }
    }
}
