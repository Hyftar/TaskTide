using System.IdentityModel.Tokens.Jwt;

namespace TaskTideAPI.Extensions
{
    public static class IHttpContextAccessorExtension
    {
        public static int GetCurrentUserId(this HttpContext HttpContext)
        {
            var stringId = HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            return int.Parse(stringId ?? "0");
        }
    }
}
