using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AnaforaWeb.Authentication
{
    public class JwtAuthenticationOptions : JwtBearerOptions
    {
        public ITicketStore? SessionStore;
    }
}
