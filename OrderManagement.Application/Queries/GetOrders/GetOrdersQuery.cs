using MediatR;

using OrderManagement.Application.DTOs;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Application.Queries.GetOrders;

public class GetOrdersQuery
    : IRequest<PaginatedResultDto<OrderDto>>
{

    public OrderStatus? Status { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}