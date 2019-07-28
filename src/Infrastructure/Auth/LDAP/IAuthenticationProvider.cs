using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.LDAP
{
    public interface IAuthenticationProvider
    {
        DomainUserDto ProvideLogin(string login, string password);
    }
}
