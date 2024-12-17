using GatewayAPI.Entities;

namespace GatewayAPI.Extensions
{
    public static class AuthenticationSchemeExtension
    {
        public static string[] GetSetFlags(this AuthenticationScheme access)
        {
            return Enum.GetValues(typeof(AuthenticationScheme))
                       .Cast<AuthenticationScheme>()
                       .Where(flag => (access & flag) == flag)
                       .Select(flag => flag.ToString())
                       .ToArray();
        }
    }
}
