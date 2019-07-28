using System.Linq;
using System.Security.Authentication;
using Auth.Adapters;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;

namespace Auth.LDAP
{
    public class LdapAuthenticationProvider : IAuthenticationProvider
    {
        private readonly LdapSettings _config;
        private readonly LdapConnection _connection;
        private readonly string[] _attributes =
        {
            "objectSid", "objectGUID", "objectCategory", "objectClass", "memberOf", "name", "cn", "distinguishedName",
            "sAMAccountName", "userPrincipalName", "displayName", "givenName", "sn", "description",
            "telephoneNumber", "mail", "streetAddress", "postalCode", "l", "st", "co", "c"
        };

        public LdapAuthenticationProvider(IOptions<LdapSettings> config)
        {
            _config = config.Value;
            _connection = new LdapConnection { SecureSocketLayer = _config.UseSSL};
        }

        public DomainUserDto ProvideLogin(string login, string password)
        {
            var domainUserName = $"{login}@{_config.DomainName}";

            _connection.Connect(_config.ServerName, _config.ServerPort);

            _connection.Bind(domainUserName, password);

            var searchFilter = string.Format(_config.SearchFilter, login);

            var result = _connection.Search(
                _config.SearchBase,
                LdapConnection.SCOPE_SUB,
                searchFilter,
                _attributes,
                false
            );

            var ldapData = result.next()?.getAttributeSet();

            if (ldapData == null)
                throw new AuthenticationException("Given login or password are not correct . Please try again");

            var user = UserAdapter.Adapt(ldapData);
            return user;
        }
    }
}
