using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Commands.Login;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;


    public AuthController(
        IMediator mediator)
    {
        _mediator = mediator;
    }


    /// <summary>
    /// Realiza login e retorna o token JWT.
    /// </summary>
    /// <param name="command">
    /// Usuário e senha.
    /// </param>
    /// <returns>
    /// Token JWT.
    /// </returns>
    [HttpPost("login")]
    [ProducesResponseType(
        typeof(LoginResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(ProblemDetails),
        StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await _mediator.Send(
                command,
                cancellationToken);


        return Ok(result);
    }
}