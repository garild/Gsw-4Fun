using System;
using System.Collections.Generic;
using System.Text;

namespace GSW.Domain.Domain.Backlogs
{
    public class BacklogEvents
    {
        public BacklogEvents(Backlog backlog)
        {
            Backlog = backlog;
        }

        public Backlog Backlog { get; }
    }

    public class CreatedEvent : BacklogEvents
    {
        public CreatedEvent(Backlog backlog) : base(backlog)
        {
        }
    }
}
