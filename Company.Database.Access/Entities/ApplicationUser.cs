using Microsoft.AspNetCore.Identity;

namespace Company.Database.Access
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isActive { get; set; }
    }
}
