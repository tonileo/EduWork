using System.ComponentModel.DataAnnotations;

namespace Common.CustomValidations
{
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime && dateTime > DateTime.Today)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success!;
        }
    }
}
