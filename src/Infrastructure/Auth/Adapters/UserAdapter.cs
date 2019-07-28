using System;
using System.Collections.Generic;
using System.Text;
using Auth.LDAP;
using Novell.Directory.Ldap;

namespace Auth.Adapters
{
    public class UserAdapter
    {
        private const string LoginAttribute = "sAMAccountName";
        private const string DisplayNameAttribute = "displayName";
        private const string SNAttribute = "sn";
        private const string GivenNameAttribute = "givenName";
        private const string MailAttribute = "mail";
        private const string TelephoneNumberAttribute = "telephoneNumber";
        private const string UserPrincipalNameAttribute = "userPrincipalName";

        public static DomainUserDto Adapt(LdapAttributeSet attributeSet)
        {
            return new DomainUserDto
            {
                Login = attributeSet.getAttribute(LoginAttribute).StringValue,
                FirstName = attributeSet.getAttribute(GivenNameAttribute).StringValue,
                LastName = attributeSet.getAttribute(SNAttribute).StringValue,
                DisplayName = attributeSet.getAttribute(DisplayNameAttribute).StringValue,
                Email = attributeSet.getAttribute(MailAttribute).StringValue,
                PhoneNumber = attributeSet.getAttribute(TelephoneNumberAttribute).StringValue,
                UserPricipalName = attributeSet.getAttribute(UserPrincipalNameAttribute).StringValue,
            };
        }
    }
}
