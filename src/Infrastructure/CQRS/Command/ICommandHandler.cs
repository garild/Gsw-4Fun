using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, out TResult> : ICommandHandler
    {
        TResult Handle(TCommand command);
    }
}
