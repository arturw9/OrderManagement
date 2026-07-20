using FluentValidation.TestHelper;

using OrderManagement.Application.Commands.CreateOrder;

namespace OrderManagement.Tests.Validators;

public class CreateOrderValidatorTests
{
    private readonly CreateOrderValidator _validator = new();

    [Fact]
    public void Deve_Retornar_Erro_Quando_CustomerId_For_Vazio()
    {
        var command = new CreateOrderCommand();

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }
}