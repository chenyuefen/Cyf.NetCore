using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TM.Core.Models;
using TM.Infrastructure.Extensions.Common;

namespace TM.Core.Identity.Authorization.Policies
{
    public class JsonWebTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public JsonWebTokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            await Response.WriteAsync(new AuthorizeResult(Code.Unauthorized,
                   Code.Unauthorized.Description()).ToJson());
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = StatusCodes.Status403Forbidden;
            await Response.WriteAsync(new AuthorizeResult(Code.Forbidden,
                   Code.Forbidden.Description()).ToJson());
        }

    }
}
