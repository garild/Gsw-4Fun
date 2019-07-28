using System;
using CQRS.Command;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Factory
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler<TCommand> Create<TCommand>()
        {
            return _serviceProvider.GetService<ICommandHandler<TCommand>>();
        }

        public ICommandHandler<TCommand, TResult> Create<TCommand, TResult>()
        {
            return _serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();
        }
    }
}
