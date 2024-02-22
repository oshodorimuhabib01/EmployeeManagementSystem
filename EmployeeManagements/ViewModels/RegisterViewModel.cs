using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace EmployeeManagements.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Action")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
