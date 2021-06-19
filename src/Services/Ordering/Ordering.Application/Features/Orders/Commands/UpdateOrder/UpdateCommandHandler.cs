﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateCommandHandler> logger;

        public UpdateCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateCommandHandler> logger)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            //get order
            var getOrderforupdate = await orderRepository.GetByIdAsync(request.Id);

            //get order
            if (getOrderforupdate == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }
            //updateOrder
            mapper.Map(request, getOrderforupdate, typeof(UpdateOrderCommand), typeof(Order));
            await orderRepository.UpdateAsync(getOrderforupdate);
            logger.LogError($"update now updated order Id : {getOrderforupdate.Id}");
            return Unit.Value;

        }
    }
}
