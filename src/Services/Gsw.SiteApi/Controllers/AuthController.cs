using Auth;
using Auth.LDAP;
using CQRS.Gate;
using GSW.Domain.Application.Command.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gsw.SiteApi.Controllers
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

        [HttpPost]
        [Route("[action]")]
        public JsonResult LoginT([FromBody] DomainUserLogCommand command)
        {
            var user = _gate.Call<DomainUserLogCommand, DomainUserDto>(command);

            _authorizeHandler.AuthorizeUser(user, HttpContext);

            return Json(user);
        }
    }
}