using Auth.DI;
using Auth.JWT;
using Auth.LDAP;
using CQRS.DI;
using Database.EF;
using DDD.DI;
using GSW.AuthApi.Formater;
using GSW.AuthApi.Middleware;
using GSW.Domain;
using GSW.Domain.Infrastructure.DatabaseContext;
using GSW.Domain.Infrastructure.DI;
using GSW.Domain.Infrastructure.Tests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GSW.AuthApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GswContext>();
            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));
            services.Configure<SqlSettings>(Configuration.GetSection("sql"));

            var assembly = GswAssemblyInformation.Assemblies;
            services.AddLdapAuth(Configuration);
            services.AddCqrs(assembly);
            services.AddDdd(assembly);
            services.AddAuthJwt(Configuration, assembly);
            services.AddGsw();

            services.AddCors(o => o.AddPolicy("AuthApiPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            services.AddMvc(p =>
            {
                p.InputFormatters.Insert(0, new RawRequestBodyFormatter());
                p.Filters.Add(new CorsAuthorizationFilterFactory("AuthApiPolicy"));
            })
            .AddJsonOptions(options=>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           
            if(Env.IsDevelopment())
            {
                services.AddScoped<IAuthenticationProvider, AuthtenticationProviderTest>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug();
                loggerFactory.AddConsole(true);
                AutoMigration(app);
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.MapWhen(p => p.Request.Headers.ContainsKey("Authorization"), 
                builder => builder.UseMiddleware<AuthorizeHandlerMiddleware>()); //TODO  This approach need to export Api GateWay
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors("AuthApiPolicy");
            app.UseMvc();
        }

        public void AutoMigration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<GswContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Database.Migrate();
            }
        }
    }
}
