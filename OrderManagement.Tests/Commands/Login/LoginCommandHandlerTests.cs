using FluentAssertions;

using Moq;

using OrderManagement.Application.Commands.Login;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Interfaces;

namespace OrderManagement.Tests.Commands.Login;

public class LoginCommandHandlerTests
{
    private readonly Mock<IJwtService> _jwtServiceMock;

    private readonly LoginCommandHandler _handler;


    public LoginCommandHandlerTests()
    {
        _jwtServiceMock = new Mock<IJwtService>();


        _handler = new LoginCommandHandler(
            _jwtServiceMock.Object);
    }



    [Fact]
    public async Task Deve_Retornar_Token_Quando_Credenciais_Forem_Validas()
    {
        // Arrange

        var response = new LoginResponseDto
        {
            Token = "fake-jwt-token",
            Expiration = DateTime.UtcNow.AddHours(1)
        };


        _jwtServiceMock
            .Setup(x =>
                x.GenerateToken("admin"))
            .Returns(response);



        var command = new LoginCommand
        {
            Username = "dev@martech.com",
            Password = "Senha@123"
        };

        // Act

        var result =
        await _handler.Handle(
            command,
            CancellationToken.None);



        // Assert

        result.Should()
            .NotBeNull();


        result.Token
            .Should()
            .Be("fake-jwt-token");


        _jwtServiceMock.Verify(
            x => x.GenerateToken("admin"),
            Times.Once);
    }



    [Fact]
    public async Task Deve_Lancar_Erro_Quando_Credenciais_Forem_Invalidas()
    {
        // Arrange

        var command = new LoginCommand
        {
            Username = "admin",
            Password = "senha_errada"
        };



        // Act

        Func<Task> action = async () =>
        {
            await _handler.Handle(
                command,
                CancellationToken.None);
        };



        // Assert

        await action
            .Should()
            .ThrowAsync<UnauthorizedAccessException>();
    }
}