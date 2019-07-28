using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Auth.JWT;
using Microsoft.Extensions.DependencyInjection;
using Auth.LDAP;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Auth.DI
{
    public static class ServiceCollection
    {
        public const string ApiAccess = "api_access";
        public const string Rol = "rol", Id = "id";

        public static void AddLdapAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LdapSettings>(configuration.GetSection("ldap"));
            services.AddScoped<IAuthenticationProvider, LdapAuthenticationProvider>();
        }
        public static void AddAuthJwt(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IJwtAuthorizeHandler, JwtAuthorizeHandler>();

            var jwtSettings = new JwtSettings();
            configuration.GetSection("jwt").Bind(jwtSettings);

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });


            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                        ValidateAudience = false,
                        ValidateLifetime = false,

                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
