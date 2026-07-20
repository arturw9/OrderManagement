using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.Entities;
public class Order
{
    private readonly List<OrderItem> _items = new();

    private Order()
    {
    }

    public Order(Guid customerId, IEnumerable<OrderItem> items)
    {
        if (customerId == Guid.Empty)
            throw new DomainException("Cliente inválido.");

        if (items is null || !items.Any())
            throw new DomainException("O pedido deve possuir pelo menos um item.");

        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;

        _items.AddRange(items);
    }

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal Total => _items.Sum(x => x.Total);

    public void Cancel()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Somente pedidos pendentes podem ser cancelados.");

        Status = OrderStatus.Cancelled;
    }
}