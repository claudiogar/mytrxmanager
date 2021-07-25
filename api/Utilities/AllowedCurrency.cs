using System.ComponentModel.DataAnnotations;
using api.Models;
using Microsoft.Extensions.DependencyInjection;

namespace api.Utilities
{
    public class AllowedCurrency : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<TransactionDbContext>();
            // TODO: check if the currency is among the supported ones (in a table to be defined)

            return ValidationResult.Success;
        }
    }
}
