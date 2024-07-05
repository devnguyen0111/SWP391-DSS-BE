using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OtherServices
{
    public class CustomJwtHandler : AuthorizationHandler<CustomJwtRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomJwtHandler> _logger;

        public CustomJwtHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<CustomJwtHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomJwtRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                var expirationDate = jwtToken.ValidTo;

                if (expirationDate < DateTime.UtcNow)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    httpContext.Response.ContentType = "application/json";
                    var responseContent = new { error = "Access token has expired" };
                    httpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(responseContent));
                    context.Fail();
                    return Task.CompletedTask;
                }

                context.Succeed(requirement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating JWT token");
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
