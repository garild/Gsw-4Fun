using Auth.LDAP;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSW.Domain.Infrastructure.Tests
{
    public class AuthtenticationProviderTest : IAuthenticationProvider
    {
        public DomainUserDto ProvideLogin(string login, string password)
        {
            var user = new DomainUserDto { FirstName = "Garib", LastName = "admin", Login = login , AssignedTeam = "1" };
            return user;
        }
    }
}
