using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Validations;

public class FutureDateAttribute : ValidationAttribute
{
    public FutureDateAttribute()
    {
        ErrorMessage = "The {0} must be today or a future date.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dataTime)
        {
            if (dataTime >= DateTime.Today)
            {
                return ValidationResult.Success;
            }
        }
        var errorMessage = string.Format(ErrorMessage, validationContext.DisplayName);
        return new ValidationResult(errorMessage);
    }
}