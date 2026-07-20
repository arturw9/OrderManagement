using MediatR;
using OrderManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Queries.GetOrderById;
public class GetOrderByIdQuery
    : IRequest<OrderDto?>
{
    public Guid Id { get; set; }

    public GetOrderByIdQuery(Guid id)
    {
        Id = id;
    }
}