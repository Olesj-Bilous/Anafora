using AnaforaData.Utils.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AnaforaWeb.Authorization
{
    public class ContentPolicyProvider : IAuthorizationPolicyProvider
    {
        public ContentPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            BackupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        private IAuthorizationPolicyProvider BackupPolicyProvider { get; }

        private const string policy_prefix = "Content";

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(ContentAuthorizeAttribute.policy_prefix, StringComparison.OrdinalIgnoreCase)
                && Enum.TryParse(typeof(Permissions), policyName.Split('_').Last(), out var permission))
            {
                return Task.FromResult(new AuthorizationPolicyBuilder(IdentityConstants.ApplicationScheme)
                    .AddRequirements(new ContentRequirement((Permissions)permission))
                    .Build());
            }

            return BackupPolicyProvider.GetPolicyAsync(policyName);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(new AuthorizationPolicyBuilder(IdentityConstants.ApplicationScheme)
                .RequireAuthenticatedUser().Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return GetDefaultPolicyAsync();
        }
    }
}
