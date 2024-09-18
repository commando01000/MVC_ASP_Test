using System.ComponentModel.DataAnnotations;

namespace MVC_ASP_Test.Models
{
    public class ResetPasswordViewModel
    {
        [Required (ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get ; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get ; set; }
        public string Email { get ; set; }
        public string Token { get ; set; }
    }
}
