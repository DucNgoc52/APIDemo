using Microsoft.AspNetCore.Identity;

namespace WebAPIClone.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { set; get; }
    }
}
