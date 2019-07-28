using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Interfaces
{
    public interface IDomainEventHandler<in T>
    {
        void Handle(T @event);
    }
}
