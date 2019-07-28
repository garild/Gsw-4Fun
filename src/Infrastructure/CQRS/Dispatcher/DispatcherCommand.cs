using CQRS.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Dispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandFactory _factory;

        public CommandDispatcher(ICommandFactory factory)
        {
            _factory = factory;
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command),
                                                "Command can not be null.");

            var commandFactory = _factory.Create<TCommand>();

            if (commandFactory == null)
                throw new InvalidOperationException($"Handler {nameof(commandFactory)} can not be null");

            commandFactory.Handle(command);
        }

        public TCommandResult Dispatch<TCommand, TCommandResult>(TCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command),
                                                "Command can not be null.");

            var commandFactory = _factory.Create<TCommand, TCommandResult>();

            if (commandFactory == null)
                throw new InvalidOperationException($"Handler {nameof(commandFactory)} can not be null");

            return commandFactory.Handle(command);
        }
    }
}
