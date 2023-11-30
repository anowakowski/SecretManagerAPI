using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecretManagerAPI.Models;

namespace SecretManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost(Name = "register")]
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
    }
}
