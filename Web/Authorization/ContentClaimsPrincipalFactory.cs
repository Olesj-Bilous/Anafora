using AnaforaData.Model;
using AnaforaData.Utils.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AnaforaWeb.Authorization
{
    public class ContentClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public ContentClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }
        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            //var sth = user.

            //if (principal?.Identity != null && user.Permissions.Any())
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(
                    new Claim("", ""));
            }
                
            return principal;
        }
    }
}
