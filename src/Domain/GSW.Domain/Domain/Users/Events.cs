using System;
using System.Collections.Generic;
using System.Text;
using DDD.Interfaces;

namespace GSW.Domain.Domain.Users
{
    public class UserEvents : IDomainEvent
    {
        public UserEvents(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }


    public class UserHasLogedEvent : UserEvents
    {
        public UserHasLogedEvent(User user):base(user)
        {
            
        }
    }
}
