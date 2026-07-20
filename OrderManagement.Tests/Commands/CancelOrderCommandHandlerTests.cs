using FluentAssertions;
using Moq;
using OrderManagement.Application.Commands.CancelOrder;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Tests.Commands;

public class CancelOrderCommandHandlerTests
{
    [Fact]
    public async Task Deve_Cancelar_Pedido()
    {
        // Arrange
        var order = new Order(
            Guid.NewGuid(),
            new List<OrderItem>
            {
                new("Notebook",1,5000)
            });

        var repository = new Mock<IOrderRepository>();

        repository.Setup(x =>
            x.GetByIdAsync(order.Id, default))
            .ReturnsAsync(order);

        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CancelOrderCommandHandler(
            repository.Object,
            unitOfWork.Object);

        // Act
        var result = await handler.Handle(
            new CancelOrderCommand(order.Id),
            default);

        // Assert
        result.Should().BeTrue();

        unitOfWork.Verify(x =>
            x.SaveChangesAsync(default),
            Times.Once);
    }
}