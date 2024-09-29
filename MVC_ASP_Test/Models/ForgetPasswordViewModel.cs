using System.ComponentModel.DataAnnotations;

namespace MVC_ASP_Test.Models
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
