using AnaforaData.Model;
using AnaforaData.Utils.Enums;
using AnaforaData.Utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AnaforaWeb.Authorization
{
    public class ContentAuthorizationHandler : AuthorizationHandler<ContentRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ContentRequirement requirement)
        {
            if (context.Resource is DefaultHttpContext filter)
            {
                HttpContext httpContext = filter.HttpContext;
                string ctrlRoute = httpContext.GetRouteValue("page").ToString(); //.Values["controller"]?.ToString();

                Claim claim = context.User.Claims.FirstOrDefault(claim => claim.Type == ctrlRoute);

                if (claim != null && Enum.TryParse(typeof(Permissions), claim.Value, out var permObj)
                    && ((Permissions)permObj & requirement.Permissions) == requirement.Permissions)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
