using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using PokemonPc.Constants;
using PokemonPc.Utils.Validators;

namespace PokemonPc.DTOs;

public record class CreateUserDto(
    [Required][StringLength(maximumLength: 50, MinimumLength = 5)] string Email,
    [Required][StringLength(maximumLength: 100, MinimumLength = 3)] string Name,
    [Required][PasswordValidation] string Password
);