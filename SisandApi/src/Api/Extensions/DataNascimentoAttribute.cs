using System.ComponentModel.DataAnnotations;

namespace Api.Extensions
{
    public class DataNascimentoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {   
            if (value is DateTime date)
            {
                if (date > DateTime.Now)
                {
                    return new ValidationResult("Data de nascimento inválida.");
                }

            }
            return ValidationResult.Success;
        }
    }
}
