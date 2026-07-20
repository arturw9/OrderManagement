using FluentAssertions;

using Moq;

using OrderManagement.Application.Commands.CreateOrder;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Tests.Commands;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Deve_Criar_Pedido()
    {
        // Arrange
        var repository = new Mock<IOrderRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateOrderCommandHandler(
            repository.Object,
            unitOfWork.Object);

        var command = new CreateOrderCommand
        {
            CustomerId = Guid.NewGuid(),
            Items =
            [
                new CreateOrderItemDto
                {
                    ProductName = "Notebook",
                    Quantity = 2,
                    UnitPrice = 3500
                }
            ]
        };

        // Act
        var id = await handler.Handle(command, CancellationToken.None);

        // Assert
        id.Should().NotBeEmpty();

        repository.Verify(x =>
            x.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()),
            Times.Once);

        unitOfWork.Verify(x =>
            x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}