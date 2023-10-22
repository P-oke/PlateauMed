using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Application.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public ValidateAgeAttribute(int minimumAge) 
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                DateTime currentDate = DateTime.Today;
                int age = currentDate.Year - dateOfBirth.Year;

                if (dateOfBirth > currentDate.AddYears(-age))
                {
                    age--;
                }

                if (age >= _minimumAge)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"The minimum age allowed is {_minimumAge} years.");
                }
            }

            return new ValidationResult("Invalid date of birth.");

        }

    }
}
