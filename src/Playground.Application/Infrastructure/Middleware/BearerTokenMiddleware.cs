using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Playground.Application.Shared.AsyncLocals;

namespace Playground.Application.Infrastructure.Middleware
{
    public class BearerTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public BearerTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var authorizationValue))
            {
                if (authorizationValue.ToString().StartsWith("Bearer "))
                {
                    var token = authorizationValue.ToString().Substring("Bearer ".Length).Trim();
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);


                    if (jwtToken.Payload.TryGetValue("UserId", out var userId))
                    {
                        UserAuthorizationContext.SetUserId(userId.ToString() ?? string.Empty);
                    }
                }
            }

            await _next(context);
        }
    }

}
