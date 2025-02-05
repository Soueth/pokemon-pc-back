using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PokemonPc.Interfaces.Utils;
using PokemonPc.Utils.Functions;

namespace PokemonPc.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public AuthService(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public string GenerateToken(string uuid)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, uuid)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            issuer: _configuration["Jwt:Issuer"]!,
            audience: _configuration["Jwt:Audience"]!,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(3),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public CookieOptions GetAuthCookieOptions()
    {
        return Encrypter.GenerateCookie
        (
            !_env.IsDevelopment(), int.Parse(_configuration["Jwt:AuthLifetime"] ?? "0")
        );
    }
}
