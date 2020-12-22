using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SourceControlFInalAssignment.CustomAnnotations
{
    public class AgeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if((Convert.ToDateTime(value)).Year < (DateTime.Now.Year - 18))
            {
                return new ValidationResult("You must be 18 years old to register !!");
            }
            else
                return ValidationResult.Success;
        }
    }
}