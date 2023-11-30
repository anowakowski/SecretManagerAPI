using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecretManagerAPI.Management.Common.Interfaces;
using SecretManagerAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecretManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthenticateController(UserManager<IdentityUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register", Name = nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status403Forbidden, new AuthenticationResponse { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status403Forbidden, new AuthenticationResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new AuthenticationResponse { Status = "Success", Message = "User created successfully!" });
        }
        
        [HttpPost("login", Name = nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = _jwtService.GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
