using AnaforaData.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AnaforaWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptionsMonitor<JwtBearerOptions> jwtOptions,
            JwtSecurityTokenHandler jwtHandler
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _jwtHandler = jwtHandler;
        }

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptionsMonitor<JwtBearerOptions> _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtHandler;

        public class SignInModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }


        public async Task<IActionResult> SignIn([FromBody] SignInModel signIn)
        {
            var user = await _userManager.FindByEmailAsync(signIn.Email);
            if (user == null) return Unauthorized();
            var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, false, false);
            if (result.Succeeded)
            {
                if (User.Identity == null) throw new Exception("User had no identity!");
                var parameters = _jwtOptions.CurrentValue.TokenValidationParameters;
                var token = new SecurityTokenDescriptor()
                {
                    Subject = (ClaimsIdentity)User.Identity,
                    Issuer = parameters.ValidIssuer,
                    Audience = parameters.ValidAudience,
                    SigningCredentials = new SigningCredentials(
                        parameters.IssuerSigningKey,
                        SecurityAlgorithms.HmacSha512Signature
                    ),
                    Expires = DateTime.UtcNow.AddMinutes(20)
                };
                return Ok(_jwtHandler.WriteToken(_jwtHandler.CreateToken(token)));
            }
            return Unauthorized();
        }
    }
}
