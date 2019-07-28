using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Interfaces
{
    public interface IEventStore
    {
        List<IDomainEvent> StoredEvents { get; set; }
    }
}
