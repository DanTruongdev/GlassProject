using System.ComponentModel.DataAnnotations;

namespace GlassECommerce.Helper.Validation
{
    public class CurrentDateValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            try
            {
                DateTime inputTime = Convert.ToDateTime(value);
                if (inputTime < DateTime.Now)
                {
                    return new ValidationResult("The input time must not be less then current date");
                }
                return ValidationResult.Success;

            }
            catch
            {
                return new ValidationResult("Invalid input");
            }

        }
    }
}
