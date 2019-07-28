using CQRS.Command;
using CQRS.Decorator;
using CQRS.Dispatcher;
using CQRS.Factory;
using CQRS.Gate;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CQRS.DI
{
    public static class ServicesCollection
    {
        public static void AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(classess => classess.AssignableTo<ICommandHandler>())
                    .AsSelf()
                    .AsImplementedInterfaces().WithScopedLifetime());

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<ICommandFactory, CommandFactory>();
            services.AddScoped<IGate, CommandGate>();
            services.Decorate<IGate, NLogCommandDecorator>();
        }
    }
}
