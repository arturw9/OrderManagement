using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using OrderManagement.Application.DTOs;
using OrderManagement.Application.Interfaces;

namespace OrderManagement.Infrastructure.Authentication;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public LoginResponseDto GenerateToken(string username)
    {
        var jwtSettings =
            _configuration
            .GetSection("Jwt");


        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                jwtSettings["Key"]!));


        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);


        var expiration =
            DateTime.UtcNow.AddHours(2);


        var claims = new[]
        {
            new Claim(
                ClaimTypes.Name,
                username)
        };


        var token =
            new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);


        return new LoginResponseDto
        {
            Token =
                new JwtSecurityTokenHandler()
                    .WriteToken(token),

            Expiration = expiration
        };
    }
}