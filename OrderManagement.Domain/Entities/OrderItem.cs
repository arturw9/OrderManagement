using OrderManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.Entities;
public class OrderItem
{
    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Order Order { get; private set; } = default!;

    public string ProductName { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal Total => Quantity * UnitPrice;

    private OrderItem()
    {
    }

    public OrderItem(
        string productName,
        int quantity,
        decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new DomainException("O nome do produto é obrigatório.");

        if (quantity <= 0)
            throw new DomainException("Quantidade inválida.");

        if (unitPrice <= 0)
            throw new DomainException("Preço inválido.");

        Id = Guid.NewGuid();
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
