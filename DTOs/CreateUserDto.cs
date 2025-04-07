using System.ComponentModel.DataAnnotations;
using PokemonPc.Utils.Validators;

namespace PokemonPc.DTOs;

public record class CreateUserDto(
    [Required][EmailAddress] string Email,
    [Required][StringLength(maximumLength: 100, MinimumLength = 3)] string Name,
    [Required][PasswordValidation] string Password
);