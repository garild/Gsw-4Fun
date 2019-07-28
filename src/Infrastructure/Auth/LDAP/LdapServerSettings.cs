namespace Auth.LDAP
{
    public class LdapSettings
    {
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
        public bool UseSSL { get; set; }
        public string SearchBase { get; set; }
        public string SearchFilter { get; set; }
        public string DomainName { get; set; }
    }
}
