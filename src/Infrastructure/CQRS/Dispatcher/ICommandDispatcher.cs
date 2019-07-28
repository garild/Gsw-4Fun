using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Dispatcher
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command);
        TCommandResult Dispatch<TCommand, TCommandResult>(TCommand command);
    }
}
