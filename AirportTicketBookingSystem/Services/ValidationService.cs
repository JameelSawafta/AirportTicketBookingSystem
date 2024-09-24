using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Services;

public static class ValidationService
{
    public static bool Validation<T>(T? validationObject,Action<T> func)
    {
        // i think we should split the validation from executing the func
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(validationObject);
        bool isValid = Validator.TryValidateObject(validationObject, validationContext, validationResults, true);
        if (isValid)
        {
            func(validationObject);
            return true;
        }
        else
        {
            Console.WriteLine($"Validation errors :");
            foreach (var validationResult in validationResults)
            {
                Console.WriteLine($"- {validationResult.ErrorMessage}");
            }

            return false;
        }
    }
}