using AnaforaData.Utils.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AnaforaWeb.Authorization
{
    public class ContentRequirement : IAuthorizationRequirement
    {
        public ContentRequirement(Permissions permissions) => Permissions = permissions;
        public Permissions Permissions { get; set; }
    }
}
