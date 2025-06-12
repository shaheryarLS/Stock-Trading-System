using AutoMapper;
using DataAccess.Entities;
using Services.DTOs;

namespace Stock_Trading_System.Profiles
{
    public class TradeProfile: Profile
    {
        public TradeProfile() 
        {
            CreateMap<Trade, TradeDto>()
                .ForMember(dest => dest.StockSymbol, opt => opt.MapFrom(src => src.Stock.Symbol))
                .ForMember(dest => dest.StockCompany, opt => opt.MapFrom(src => src.Stock.CompanyName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<CreateTradeDto, Trade>();
        }
    }
}
