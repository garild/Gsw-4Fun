using System;
using System.Collections.Generic;
using System.Text;
using DDD.Interfaces;
using GSW.Domain.Domain.Users;

namespace GSW.Domain.Domain.Teams
{
    public class Team
    {
        private IDomainEventDispatcher _domainEventDispatcher;
        public static Team Create(int id,string name, int createBy, DateTime createAt, IDomainEventDispatcher domainEventDispatcher)
        {
            return new Team
            {
                Id = id,
                Name = name,
                CreateBy = createBy,
                CreateAt = createAt,
                _domainEventDispatcher = domainEventDispatcher
            };
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CreateBy { get; private set; }
        public DateTime CreateAt { get; private set; }
    }
}
