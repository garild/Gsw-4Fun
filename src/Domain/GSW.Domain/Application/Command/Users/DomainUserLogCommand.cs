using System;
using System.Collections.Generic;
using System.Text;

namespace GSW.Domain.Application.Command.Users
{
    public class DomainUserLogCommand
    {
        public DomainUserLogCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public string Login { get; }
        public string Password { get; }
    }
}

