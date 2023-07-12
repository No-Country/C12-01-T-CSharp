using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateJwtToken(AppUser user)
        {
            // Create an array of Claim objects representing the user's claims, such as their ID, name, email, etc.
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Subject (user id)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT unique ID
                new Claim(JwtRegisteredClaimNames.Iat,
                    DateTime.UtcNow.ToString()), //Issued at (date and time of token generation)

                //new Claim(ClaimTypes.NameIdentifier, user.Email), //Unique name identifier of the user (Email)
                new Claim(ClaimTypes.Name, user.UserName), //Name of the user
                //new Claim(ClaimTypes.Role,role )

            };

            // Create a SymmetricSecurityKey object using the key specified in the configuration.
            SymmetricSecurityKey securityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Create a SigningCredentials object with the security key and the HMACSHA256 algorithm.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create a JwtSecurityToken object with the given issuer, audience, claims, expiration, and signing credentials.
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                signingCredentials: signingCredentials
            );

            // Create a JwtSecurityTokenHandler object and use it to write the token as a string.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            // Create and return an AuthenticationResponse object containing the token, user email, user name, and token expiration time.
            return token;
        }
    }
}
