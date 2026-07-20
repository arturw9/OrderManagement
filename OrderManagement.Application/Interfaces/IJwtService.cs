using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Interfaces;

public interface IJwtService
{
    LoginResponseDto GenerateToken(string username);
}