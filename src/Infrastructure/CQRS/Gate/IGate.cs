using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Gate
{
    public interface IGate
    {
        void Call<TCommand>(TCommand command);
        TResult Call<TCommand,TResult>(TCommand command);
    }
}
