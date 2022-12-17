using AnaforaData.Model;
using AnaforaData.Utils.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AnaforaWeb.Authorization
{
    public class ContentAuthorizeAttribute : AuthorizeAttribute
    {
        const string policy_prefix = "Content";

        public ContentAuthorizeAttribute(Permissions permissions)
        {
            Policy = string.Join('_', policy_prefix, permissions);
        }
    }
}
