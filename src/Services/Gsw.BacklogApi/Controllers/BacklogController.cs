using CQRS.Gate;
using GSW.Domain.Application.Command.Backlogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gsw.BacklogApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BacklogController
    {
        private readonly IGate _gate;

        public BacklogController(IGate gate)
        {
            _gate = gate;
        }

        [HttpPost]
        [Route("[action]")]
        public void CreateBacklog([FromBody] CreateBacklogCommand command)
        {
            _gate.Call(command);
        }
      
    }
}
