using System.ComponentModel.DataAnnotations;
using PokemonPc.Constants;

namespace PokemonPc.Utils.Validators;

public class PasswordValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string stringValue)
        {
            return new ValidationResult($"{validationContext.DisplayName} must be a valid string");
        }

        if (stringValue.Length < 8 || stringValue.Length > 80)
        {
            return new ValidationResult($"{validationContext.DisplayName} must be bigger than 8 and less than 80 characters");
        }

        if (
            !stringValue.Any(char.IsUpper) || 
            !stringValue.Any(char.IsDigit) ||
            !stringValue.Any(char.IsLower) ||
            !stringValue.Any(c => !char.IsLetterOrDigit(c))
        ) {
            return new ValidationResult
            (
                $"{validationContext.DisplayName} must have at last a digit, a capital letter, a lowercase and a symbol"
            );
        }

        return ValidationResult.Success;
    }
}
