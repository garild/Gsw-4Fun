using Auth.LDAP;
using GSW.Domain.Domain.Users;
using GSW.Domain.Infrastructure.DatabaseContext;

namespace GSW.Domain.Infrastructure.Repository.Users
{
    public interface IUserServices
    {
        User CreateUser(DomainUserDto domainUserDto, string password);
        User FindByLogin(string login);
        GswContext Context { get; }
    }
}
