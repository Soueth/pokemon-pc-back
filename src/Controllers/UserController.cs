using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PokemonPc.DTOs;
using PokemonPc.Interfaces.Services;
using PokemonPc.Mapping;
using PokemonPc.Models;

namespace PokemonPc.src.Controllers;
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost(Name = "CreateUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        User user = await _userService.CreateUser(userDto);

        return Results.CreatedAtRoute
        (
            nameof(CreateUser),
            new { id = user.Id }, 
            user.ToDto(user.Trainer?.Name!)
        );
    }
}
