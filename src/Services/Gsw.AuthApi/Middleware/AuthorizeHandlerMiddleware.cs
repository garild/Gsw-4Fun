using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Auth.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using System.Security.Authentication;
using Auth;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GSW.AuthApi.Middleware
{
    public class AuthorizeHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public AuthorizeHandlerMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
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
                        var token = sessionHeader.Substring(JwtBearerDefaults.AuthenticationScheme.Length);
                        if (authorizeHandler.TokenExpired(token)) //TODO while token expired time is less than 1 min, refresh token
                        {
                            context.Response.StatusCode = 401;
                            throw new AuthenticationException("Token expired");
                        }
                    }
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
