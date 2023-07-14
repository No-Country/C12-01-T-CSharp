using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) :base(options)
        {
            
        }


    }
}
