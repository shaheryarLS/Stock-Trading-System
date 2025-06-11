using AutoMapper;
using DataAccess.Entities;
using Services.DTOs;

namespace Stock_Trading_System.Profiles
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<CreateStockDto, Stock>();
        }
    }
}
