using AnaforaData.Utils.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AnaforaWeb.Authorization
{
    public class ContentPolicyProvider : IAuthorizationPolicyProvider
    {
        const string policy_prefix = "Content";

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(policy_prefix, StringComparison.OrdinalIgnoreCase)
                && Enum.TryParse(typeof(Permissions), policyName.Split('_').Last(), out var permission))
            {
                return Task.FromResult(new AuthorizationPolicyBuilder(policyName)
                    .AddRequirements(new ContentRequirement((Permissions)permission))
                    .Build());
            }

            return GetDefaultPolicyAsync();
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
