using Microsoft.AspNetCore.Mvc;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Services;
using PokemonPc.Interfaces.Utils;
using PokemonPc.Utils.Functions;

namespace PokemonPc.Controllers;
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UserController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("create-user", Name = nameof(CreateUser))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        UserTokenDto user = await _userService.CreateUser(userDto);

        Response.Headers.TryAdd("Authorization", $"Bearer {user.Token}");

        // Response.Cookies.Append("RefreshToken", user.Token, _authService.GetAuthCookieOptions());

        return Results.CreatedAtRoute
        (
            nameof(CreateUser),
            new { id = user.Id }, 
            user
        );
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        string token = await _userService.Login(loginDto);

        Response.Headers.TryAdd("Authorization", $"Bearer {token}");

        return Ok(new { message = "Login bem sucedido" });
    }
}
