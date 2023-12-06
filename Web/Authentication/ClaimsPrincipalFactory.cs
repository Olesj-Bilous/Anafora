using AnaforaData.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AnaforaWeb.Authentication
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public ClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        public const string UserIdClaimType = "user-id";
        public const string SessionIdClaimType = "session-id";

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            ClaimsIdentity claims = await base.GenerateClaimsAsync(user);
            claims.AddClaim(new Claim(UserIdClaimType, user.Id.ToString()));
            return claims;
        }
    }
}
