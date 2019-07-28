using System;
using Newtonsoft.Json;

namespace GSW.Domain.Application.Command.Backlogs
{
    public class CreateBacklogCommand
    {
        [JsonConstructor]
        public CreateBacklogCommand(string title, string type, string priority, string status, string description, string assigneeUser, string reporterUser, DateTime createAt)
        {
            Title = title;
            Type = type;
            Priority = priority;
            Status = status;
            Description = description;
            AssigneeUser = assigneeUser;
            ReporterUser = reporterUser;
            CreateAt = createAt;
        }

        public string Title { get; }
        public string Type { get; }
        public string Priority { get; }
        public string Status { get; }
        public string Description { get; }
        public string AssigneeUser { get; }
        public string ReporterUser { get; }
        public DateTime CreateAt { get; }
    }
}
