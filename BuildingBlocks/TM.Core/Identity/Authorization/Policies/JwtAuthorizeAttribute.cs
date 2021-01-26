using Microsoft.AspNetCore.Authorization;

namespace TM.Core.Identity.Authorization.Policies
{
    /// <summary>
    /// Jwt授权
    /// </summary>
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
    }
}
