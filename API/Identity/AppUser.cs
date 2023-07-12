using Microsoft.AspNetCore.Identity;

namespace API.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
