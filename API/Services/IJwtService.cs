using API.Identity;

namespace API.Services
{
    public interface IJwtService
    {
        string CreateJwtToken(AppUser user);

    }
}
