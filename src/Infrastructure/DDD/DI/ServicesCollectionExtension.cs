using DDD.Implementation;
using DDD.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DDD.DI
{
    public static class ServicesCollectionExtension
    {
        public static void AddDdd(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            services.Scan(scan =>
            scan.FromAssemblies(assemblies)
            .AddClasses(classess => classess.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        }
    }
}
