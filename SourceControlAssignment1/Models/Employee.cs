using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SourceControlAssignment1.Models
{
    [Bind(Exclude = "Id")]
    public class Employee
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 5)]
        [DisplayName("Employee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email id is required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [DisplayName("Email-id")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You have to confirm email id")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Confirm Email-id")]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "Email does not match")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [Range(20, 50, ErrorMessage = "Age must be between 20-50 in years.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [DisplayName("Department")]
        [SourceControlAssignment1.CustomAttributes.Department()]
        public string Dept { get; set; }
    }
}