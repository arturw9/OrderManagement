using MediatR;

using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }

    public List<CreateOrderItemDto> Items { get; set; } = [];
}