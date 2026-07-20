using FluentAssertions;

using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Deve_Cancelar_Pedido_Pendente()
    {
        // Arrange
        var order = new Order(
            Guid.NewGuid(),
            new List<OrderItem>
            {
                new("Notebook", 1, 5000)
            });

        // Act
        order.Cancel();

        // Assert
        order.Status.Should().Be(OrderStatus.Cancelled);
    }

    [Fact]
    public void Nao_Deve_Cancelar_Pedido_Ja_Cancelado()
    {
        // Arrange
        var order = new Order(
            Guid.NewGuid(),
            new List<OrderItem>
            {
                new("Notebook",1,5000)
            });

        order.Cancel();

        // Act
        Action act = () => order.Cancel();

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Somente pedidos pendentes podem ser cancelados.");
    }
}