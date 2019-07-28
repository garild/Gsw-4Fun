using Auth.LDAP;
using Microsoft.AspNetCore.Http;

namespace Auth
{
    public interface IJwtAuthorizeHandler
    {
        void AuthorizeUser(DomainUserDto userDto, HttpContext context);
        bool TokenExpired(string token);
    }
}