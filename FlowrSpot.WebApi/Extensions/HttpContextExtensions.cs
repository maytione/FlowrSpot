using System.Security.Claims;

namespace FlowrSpot.WebApi.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetIdentityIdClaimValue(this HttpContext context)
        {
            return GetGuidClaimValue("IdentityId", context);
        }

        private static string GetGuidClaimValue(string key, HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            return identity?.FindFirst(key)?.Value!;
        }
    }
}
