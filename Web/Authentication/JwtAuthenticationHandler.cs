using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace AnaforaWeb.Authentication
{
    public class JwtAuthenticationHandler : SignInAuthenticationHandler<JwtAuthenticationOptions>
    {
        public JwtAuthenticationHandler(IOptionsMonitor<JwtAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            BearerHandler = new(options, logger, encoder, clock);
        }

        protected JwtBearerHandler BearerHandler { get; set; }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return BearerHandler.AuthenticateAsync();
        }

        protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
        {
            throw new NotImplementedException();
        }

        protected override Task HandleSignOutAsync(AuthenticationProperties? properties)
        {
            throw new NotImplementedException();
        }
    }
}
