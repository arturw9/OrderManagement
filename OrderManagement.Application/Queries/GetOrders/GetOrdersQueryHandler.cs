using AutoMapper;
using MediatR;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Queries.GetOrders;

public class GetOrdersQueryHandler
    : IRequestHandler<
        GetOrdersQuery,
        PaginatedResultDto<OrderDto>>
{

    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;


    public GetOrdersQueryHandler(
        IOrderRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }



    public async Task<PaginatedResultDto<OrderDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {


        var result = await _repository.GetAllAsync(
            request.Status,
            request.Page,
            request.PageSize,
            cancellationToken);

        return new PaginatedResultDto<OrderDto>
        {
            Items = _mapper.Map<List<OrderDto>>(result.Items),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalItems = result.TotalItems
        };
    }
}