using CQRS.Gate;
using GSW.Domain.Application.Command.Backlogs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gsw.SiteApi.Controllers
{
    [Route("api/[controller]")]
    public class BacklogController : Controller
    {
        private readonly IGate _gate;

        public BacklogController(IGate gate)
        {
            _gate = gate;
        }
        [HttpPost]
        [Route("[action]")]
        public string Index()
        {
            return "Works";
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public void Create([FromBody] CreateBacklogCommand command)
        {
            _gate.Call(command);
        }
      
    }
}
