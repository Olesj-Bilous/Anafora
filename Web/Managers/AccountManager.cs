using AnaforaData.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace AnaforaWeb.Managers
{
    public class AccountManager
    {
        public AccountManager(
            UserManager<User> userManager,
            IOptionsMonitor<JwtBearerOptions> jwtOptions,
            JwtSecurityTokenHandler jwtHandler,
            IUserClaimsPrincipalFactory<User> principalFactory
        )
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _jwtHandler = jwtHandler;
            _principalFactory = principalFactory;
        }

        private readonly UserManager<User> _userManager;
        private readonly IOptionsMonitor<JwtBearerOptions> _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtHandler;
        private readonly IUserClaimsPrincipalFactory<User> _principalFactory;
    }
}
