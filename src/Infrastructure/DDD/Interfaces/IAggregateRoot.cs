using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Interfaces
{
    public interface IAggregateRoot
    {
        List<IDomainEvent> DomainEvents { get; }
    }
}
