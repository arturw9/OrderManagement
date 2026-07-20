using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Commands.CreateOrder;
public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.Items)
            .NotEmpty();

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(i => i.ProductName)
                    .NotEmpty();

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0);

                item.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0);
            });
    }
}