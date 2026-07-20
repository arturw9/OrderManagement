using MediatR;

using OrderManagement.Application.DTOs;
using OrderManagement.Application.Interfaces;

namespace OrderManagement.Application.Commands.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IJwtService _jwtService;


    public LoginCommandHandler(
        IJwtService jwtService)
    {
        _jwtService = jwtService;
    }


    public Task<LoginResponseDto> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Username != "dev@martech.com" ||
            request.Password != "Senha@123")
        {
            throw new UnauthorizedAccessException(
                "Usuário ou senha inválidos.");
        }


        var token =
            _jwtService.GenerateToken(
                request.Username);


        return Task.FromResult(token);
    }
}