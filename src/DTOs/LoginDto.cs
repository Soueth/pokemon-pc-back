using System.ComponentModel.DataAnnotations;
using PokemonPc.Utils.Validators;

namespace PokemonPc.DTOs;

public record class LoginDto(
    [Required][EmailAddress] string Email,
    [Required][PasswordValidation] string Password
);

