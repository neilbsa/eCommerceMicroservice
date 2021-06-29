﻿using AutoMapper;
using EventBus.Message.Events;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Mapping
{
    public class OrderingProfile : Profile
    {

        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
