using FluentValidation;

namespace OrderManagement.Application.Commands.Login;

public class LoginValidator
    : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Usuário obrigatório.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha obrigatória.");
    }
}