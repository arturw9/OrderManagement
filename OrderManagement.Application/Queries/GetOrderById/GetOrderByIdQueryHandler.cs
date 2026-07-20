using AutoMapper;
using MediatR;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Queries.GetOrderById;
public class GetOrderByIdQueryHandler
    : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{

    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;


    public GetOrderByIdQueryHandler(
        IOrderRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }



    public async Task<OrderDto?> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {

        var order = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);


        if (order is null)
            return null;


        return _mapper.Map<OrderDto>(
            order);
    }
}
