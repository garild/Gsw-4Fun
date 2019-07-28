using System;
using CQRS.Command;
using GSW.Domain.Application.Command.Backlogs;
using GSW.Domain.Domain.Backlogs;
using GSW.Domain.Domain.Backlogs.Shared;
using GSW.Domain.Infrastructure.DatabaseContext;

namespace GSW.Domain.Application.CommandHandlers.Backlogs
{
    public class BacklogCreateCommandHandler : ICommandHandler<CreateBacklogCommand>
    {
        private readonly GswContext _gswContext;

        public BacklogCreateCommandHandler(GswContext gswContext)
        {
            _gswContext = gswContext;
        }
        public void Handle(CreateBacklogCommand command)
        {
            var backlog = Backlog.Create(0, command.Title, Enum.Parse<BacklogTypeEnum>(command.Type),Enum.Parse<BacklogPriorityEnumType>(command.Priority), Enum.Parse<BacklogStatusEnum>(command.Status), ResolutionEnumType.New, command.Description, null, null, DateTime.Today, null);
            _gswContext.Backlogs.Add(backlog);

            _gswContext.SaveChanges();
            
        }
    }
}
