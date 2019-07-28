using CQRS.Dispatcher;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Gate
{
    public class CommandGate : IGate
    {
        public readonly ICommandDispatcher _commandDispatcher;

        public CommandGate(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void Call<TCommand>(TCommand command)
        {
            _commandDispatcher.Dispatch<TCommand>(command);
        }

        public TResult Call<TCommand, TResult>(TCommand command)
        {
            return _commandDispatcher.Dispatch<TCommand, TResult>(command);
        }
    }
}
