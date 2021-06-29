using AutoMapper;
using Basket.API.Entities;
using EventBus.Message.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckOut, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
