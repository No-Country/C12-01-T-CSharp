using Microsoft.AspNetCore.Identity;

namespace API.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
