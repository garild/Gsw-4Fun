using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using GSW.Domain.Infrastructure.Repository.Users;

namespace GSW.Domain.Infrastructure.DI
{
    public static class ServiceCollection
    {
        public static void AddGsw(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
        }
    }
}
