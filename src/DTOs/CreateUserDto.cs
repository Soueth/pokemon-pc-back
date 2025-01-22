using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PokemonPc.DTOs;

public record class CreateUserDto(
    [Required][StringLength(maximumLength: 50, MinimumLength = 5)] string Email,
    // [Required][ObjectIdValidator] string Trainer,
    [Required][StringLength(maximumLength: 100, MinimumLength = 3)] string Name
);