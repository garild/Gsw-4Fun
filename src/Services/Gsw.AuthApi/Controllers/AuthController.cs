using System.Security.Claims;
using Auth;
using Auth.JWT;
using Auth.LDAP;
using CQRS.Gate;
using GSW.Domain.Application.Command;
using GSW.Domain.Application.Command.Backlogs;
using GSW.Domain.Application.Command.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSW.AuthApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IGate _gate;
        private readonly IJwtAuthorizeHandler _authorizeHandler;

        public AuthController(IGate gate, IJwtAuthorizeHandler authorizeHandler)
        {
            _gate = gate;
            _authorizeHandler = authorizeHandler;
        }

        public string Index()
        {
            return "Hellow World";
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public JsonResult Login([FromBody] DomainUserLogCommand command)
        {
            var user = _gate.Call<DomainUserLogCommand, DomainUserDto>(command);

            _authorizeHandler.AuthorizeUser(user,HttpContext);

            return Json(user);
        }
    }
}