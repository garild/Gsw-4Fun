using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Interfaces
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<T>(T domainEvent);
        void DispatchEvents<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}
