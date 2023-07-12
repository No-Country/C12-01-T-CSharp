using API.Dtos;
using API.Identity;
using API.Services;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _tokenService;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IJwtService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult("Email address is in use");
            }

            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                return BadRequest(errors);
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateJwtToken(user),
                Email = user.Email
            };
        }

        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateJwtToken(user),
                UserName = user.UserName
            };
        }

        /// <summary>
        /// Check if the email exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

    }
}
