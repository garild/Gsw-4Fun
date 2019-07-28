using System;
using System.Collections.Generic;
using System.Text;
using DDD.Interfaces;
using GSW.Domain.Domain.Users;

namespace GSW.Domain.Application.EventHandlers
{
    public class UserHasLogedEventHandler : IDomainEventHandler<UserHasLogedEvent>
    {
        public UserHasLogedEventHandler()
        {
            
        }

        public void Handle(UserHasLogedEvent @event)
        {
            //TODO Catch event and save ? ES ?
            //throw new NotImplementedException();
        }
    }
}
