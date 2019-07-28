using System;
using System.Linq;
using Auth.JWT;
using Auth.LDAP;
using DDD.Interfaces;
using GSW.Domain.Domain.Users;
using GSW.Domain.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GSW.Domain.Infrastructure.Repository.Users
{
    public class UserServices : IUserServices
    {
        public GswContext Context { get; }
        private readonly IPasswordHasher<DomainUserDto> _passwordHasher;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        public UserServices(GswContext context,IDomainEventDispatcher domainEventDispatcher)
        {
            Context = context;
            _passwordHasher = new PasswordHasher<DomainUserDto>();
            _domainEventDispatcher = domainEventDispatcher;
        }

        public User CreateUser(DomainUserDto domainUserDto,string password)
        {
            var hashedPassword = _passwordHasher.HashPassword(domainUserDto, password);
            var newUser = User.Create(null,domainUserDto.Login, domainUserDto.FirstName, domainUserDto.LastName,domainUserDto.DisplayName,null, domainUserDto.UserPricipalName,
                hashedPassword, domainUserDto.PhoneNumber, domainUserDto.Email, null, AccountStatusEnum.Active,
                null, null);

            Context.Users.Add(newUser);

            return newUser;
        }

        public User FindByLogin(string login)
        {
            var searchUser =
                Context.Users
                    .Include(p=>p.Claims)
                    .Include(p => p.AssignedTeam)
                    .FirstOrDefault(p => p.Login.Equals(login, StringComparison.OrdinalIgnoreCase));

            return searchUser;
        }
    }
}
