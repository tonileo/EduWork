using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
