using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Gsw.SiteApi.Middleware
{
    public class AuthorizeHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizeHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtAuthorizeHandler authorizeHandler)
        {
            try
            {
                if (!context.Request.Path.Value.EndsWith("login"))
                {
                    var sessionHeader = context.Request.Headers["authorization"].ToString();

                    if (string.IsNullOrEmpty(sessionHeader))
                    {
                        context.Response.StatusCode = 401;
                        throw new AuthenticationException();
                    }
                    if (sessionHeader.StartsWith(JwtBearerDefaults.AuthenticationScheme))
                    {
                        var token = sessionHeader.Substring(JwtBearerDefaults.AuthenticationScheme.Length+1);
                        if (authorizeHandler.TokenExpired(token)) //TODO while token expired time is less than 1 min, refresh token
                        {
                            context.Response.StatusCode = 401;
                            throw new AuthenticationException("Token expired");
                        }
                    }

                    await _next(context);
                }
                else
                    await _next(context);

            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = context.Response.StatusCode != 200 ? context.Response.StatusCode : 400;

            var response = new { message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);

            return context.Response.WriteAsync(payload);
        }
    }
}
