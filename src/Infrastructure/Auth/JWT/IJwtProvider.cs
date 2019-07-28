
using Auth.LDAP;

namespace Auth.JWT
{
    public interface IJwtProvider
    {
        JsonWebToken Create(DomainUserDto userDto, string[] userRole);
    }
}
