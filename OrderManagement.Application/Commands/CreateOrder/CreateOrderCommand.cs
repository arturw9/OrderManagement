using MediatR;
using OrderManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Commands.CreateOrder;
public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }

    public List<CreateOrderItemDto> Items { get; set; } = [];
}