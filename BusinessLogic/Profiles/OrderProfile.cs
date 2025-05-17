using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;
using MenuManager.DB.Models;

namespace BusinessLogic.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderBusinessModel, Order>()
            .ForMember(dest => dest.dishes, opt => opt.MapFrom(src => src.dishes))
            .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
            .ReverseMap();
        }
    }
}
