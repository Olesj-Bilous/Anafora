using AnaforaData.Model;
using AnaforaWeb.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace AnaforaWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        public AccountController(
            UserManager<User> userManager,
            IOptionsMonitor<JwtBearerOptions> jwtOptions,
            JwtSecurityTokenHandler tokenHandler,
            IUserClaimsPrincipalFactory<User> principalFactory
        )
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _tokenHandler = tokenHandler;
            _principalFactory = principalFactory;
        }

        private readonly UserManager<User> _userManager;
        private readonly IOptionsMonitor<JwtBearerOptions> _jwtOptions;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IUserClaimsPrincipalFactory<User> _principalFactory;

        public class SignInModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signIn)
        {
            var user = await _userManager.FindByEmailAsync(signIn.Email);
            if (user == null) return Unauthorized();
            var valid = await _userManager.CheckPasswordAsync(user, signIn.Password);
            if (valid)
            {
                var principal = await _principalFactory.CreateAsync(user);
                var parameters = _jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
                var token = new SecurityTokenDescriptor()
                {
                    Subject = principal.Identities.First(),
                    Issuer = parameters.ValidIssuer,
                    Audience = parameters.ValidAudience,
                    SigningCredentials = new SigningCredentials(
                        parameters.IssuerSigningKey,
                        SecurityAlgorithms.HmacSha512Signature
                    ),
                    Expires = DateTime.UtcNow.AddMinutes(20)
                };
                token.Claims.Add(ClaimsPrincipalFactory.SessionIdClaimType, HttpContext.Session.Id);
                return Ok(_tokenHandler.WriteToken(_tokenHandler.CreateToken(token)));
            }
            return Unauthorized();
        }
    }
}
