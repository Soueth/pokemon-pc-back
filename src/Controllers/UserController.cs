using Microsoft.AspNetCore.Mvc;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Services;

namespace PokemonPc.Controllers;
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create-user")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        UserTokenDto user = await _userService.CreateUser(userDto);
    
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

        return Ok(new { message = "token adicionado com sucesso" });
    }
}
