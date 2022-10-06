using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Api.Extensions.User
{
    [ExcludeFromCodeCoverage]
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId( this ClaimsPrincipal principal )
        {
            if (principal == null)
            {
                throw new ArgumentException("Not found", nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail( this ClaimsPrincipal principal )
        {
            if (principal == null)
            {
                throw new ArgumentException("Not found", nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
