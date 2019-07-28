using Auth.DI;
using Auth.JWT;
using CQRS.DI;
using Database.EF;
using DDD.DI;
using Gsw.SiteApi.Formater;
using Gsw.SiteApi.Middleware;
using GSW.Domain;
using GSW.Domain.Infrastructure.DatabaseContext;
using GSW.Domain.Infrastructure.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gsw.SiteApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());

            });
            services.AddDbContext<GswContext>();
            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));
            services.Configure<SqlSettings>(Configuration.GetSection("sql"));

            var assembly = GswAssemblyInformation.Assemblies;
            services.AddLdapAuth(Configuration);
            services.AddCqrs(assembly);
            services.AddDdd(assembly);
            services.AddAuthJwt(Configuration, assembly);
            services.AddGsw();

            services.AddMvc(config =>
                {
                    config.InputFormatters.Insert(0, new RawRequestBodyFormatter());
                   
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug();
                loggerFactory.AddConsole(true);
                AutoMigration(app);
            }

            app.UseHttpsRedirection();

            //app.MapWhen(p => p.Request.Headers.ContainsKey("Authorization"),
            //    builder => builder.UseMiddleware<AuthorizeHandlerMiddleware>()); //TODO  This approach need to export Api GateWay
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors("AllowAll");
            app.UseAuthentication();
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
