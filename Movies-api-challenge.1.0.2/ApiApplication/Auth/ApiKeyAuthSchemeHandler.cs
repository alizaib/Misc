﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static ApiApplication.Constants;

namespace ApiApplication.Auth
{
    public class ApiKeyAuthSchemeHandler : AuthenticationHandler<ApiKeyAuthSchemeOptions>
    {
        public ApiKeyAuthSchemeHandler(IOptionsMonitor<ApiKeyAuthSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var apiKey = Context.Request.Headers[Headers.ApiKey];
            if (apiKey != Options.ApiKey)
            {
                return Task.FromResult(AuthenticateResult.Fail($"Invalid {Headers.ApiKey}"));
            }
            var claims = new[] { new Claim(ClaimTypes.Name, "VALID USER") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
