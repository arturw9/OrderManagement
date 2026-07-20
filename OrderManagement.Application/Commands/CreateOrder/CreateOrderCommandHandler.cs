using MediatR;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Commands.CreateOrder;
public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var items = request.Items
            .Select(x =>
                new OrderItem(
                    x.ProductName,
                    x.Quantity,
                    x.UnitPrice))
            .ToList();

        var order = new Order(
            request.CustomerId,
            items);

        await _repository.AddAsync(
            order,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return order.Id;
    }
}