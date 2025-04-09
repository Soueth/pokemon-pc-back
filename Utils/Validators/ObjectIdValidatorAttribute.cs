using MongoDB.Bson;

namespace System.ComponentModel.DataAnnotations;

public class ObjectIdValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string stringValue && ObjectId.TryParse(stringValue, out _))
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("O ID provido não é um ObjectId válido");
    }
}
