using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DDD.Interfaces;
using GSW.Domain.Domain.Backlogs.Shared;
using GSW.Domain.Domain.Users;

namespace GSW.Domain.Domain.Backlogs
{
    public class Backlog : IAggregateRoot
    {
        [NotMapped]
        public List<IDomainEvent> DomainEvents { get; set; } = new List<IDomainEvent>();

        public static Backlog Create(int id, string title, BacklogTypeEnum type,
            BacklogPriorityEnumType priority, BacklogStatusEnum status,
            ResolutionEnumType resolution, string description,
            User assigneeUser, User reporterUser,
            DateTime createAt, DateTime? updatedAt)
        {
            return new Backlog
            {
                Id = id,
                Title = title,
                Type = type,
                Priority = priority,
                Status = status,
                Resolution = resolution,
                Description = description,
                AssignedUser = assigneeUser,
                ReporterUser = reporterUser,
                CreateAt = createAt,
                UpdatedAt = updatedAt,
            };
        }

        private Backlog() { }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public BacklogTypeEnum Type { get; private set; }
        public BacklogPriorityEnumType Priority { get; private set; }
        public BacklogStatusEnum Status { get; private set; }
        public ResolutionEnumType Resolution { get; private set; }
        public string Description { get; private set; }
        public User AssignedUser { get; private set; }
        public User ReporterUser { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
      
    }
}
