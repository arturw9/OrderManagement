using MediatR;

using OrderManagement.Application.DTOs;

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