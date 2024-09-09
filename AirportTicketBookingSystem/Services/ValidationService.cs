using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Services;

public class ValidationService
{
    public static void Validation(object? validationObject,Action<object> func)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(validationObject);
        bool isValid = Validator.TryValidateObject(validationObject, validationContext, validationResults, true);
        if (isValid)
        {
            func(validationObject);
        }
        else
        {
            Console.WriteLine($"Validation errors :");
            foreach (var validationResult in validationResults)
            {
                Console.WriteLine($"- {validationResult.ErrorMessage}");
            }
        }
    }
}