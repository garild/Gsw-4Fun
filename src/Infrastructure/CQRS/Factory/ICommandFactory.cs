using CQRS.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Factory
{
    public interface ICommandFactory
    {
        ICommandHandler<TCommand> Create<TCommand>();
        ICommandHandler<TCommand,TResult> Create<TCommand, TResult>();
    }
}
