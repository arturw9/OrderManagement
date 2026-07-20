using OrderManagement.Application.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FluentValidation.TestHelper;

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