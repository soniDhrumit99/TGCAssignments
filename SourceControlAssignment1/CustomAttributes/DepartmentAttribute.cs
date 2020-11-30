using System;
using System.ComponentModel.DataAnnotations;

namespace SourceControlAssignment1.CustomAttributes
{
    public class DepartmentAttribute : ValidationAttribute
    {
        private string[] _dept = { "HR", "IT", "Engineering", "Admin", "Marketing" };

        public DepartmentAttribute() : base("This does not match with any valid department") {}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null && value is string)
            {
                if(Array.IndexOf(_dept, value) == -1)
                {
                    var errorMessage = FormatErrorMessage("This does not match with any valid department");
                    return new ValidationResult(errorMessage);
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("This does not match with any valid department");
        }
    }
}