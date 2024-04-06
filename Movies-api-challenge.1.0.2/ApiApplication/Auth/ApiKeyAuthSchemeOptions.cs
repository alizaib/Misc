using Microsoft.AspNetCore.Authentication;

namespace ApiApplication.Auth
{
    public class ApiKeyAuthSchemeOptions : AuthenticationSchemeOptions
    {
        public string ApiKey { get; set; }
    }

    public static class ApiKey
    {
        public const string Scheme = "ApiKeyScheme";
    }
}
