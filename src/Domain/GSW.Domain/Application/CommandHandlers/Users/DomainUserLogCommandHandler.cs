using System.Linq;
using Auth.LDAP;
using CQRS.Command;
using GSW.Domain.Application.Command.Users;
using GSW.Domain.Infrastructure.Repository.Users;

namespace GSW.Domain.Application.CommandHandlers.Users
{
    internal class DomainUserLogCommandHandler : ICommandHandler<DomainUserLogCommand, DomainUserDto>
    {
        private readonly IAuthenticationProvider _authenticationProvide;
        private readonly IUserServices _userServices;

        public DomainUserLogCommandHandler(IAuthenticationProvider authenticationProvide,IUserServices userServices)
        {
            _authenticationProvide = authenticationProvide;
            _userServices = userServices;
        }

        public DomainUserDto Handle(DomainUserLogCommand command)
        {
            var loggedUser = _authenticationProvide.ProvideLogin(command.Login, command.Password);

            var user = _userServices.FindByLogin(command.Login) ?? _userServices.CreateUser(loggedUser, command.Password);

            user.UserHasLoged();

            loggedUser.Roles = user.Claims?.Select(p=>  p.Role.ToString()).ToArray();
            loggedUser.AssignedTeam = user.AssignedTeam?.Name;
            loggedUser.AccountStatus = user.Status.ToString();

            _userServices.Context.SaveChanges();

            return loggedUser;
        }
    }
}
