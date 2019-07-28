using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DDD.Interfaces;
using GSW.Domain.Domain.Teams;

namespace GSW.Domain.Domain.Users
{
    public class User : IAggregateRoot
    {
        [NotMapped]
        public List<IDomainEvent> DomainEvents { get; set; } = new List<IDomainEvent>();

        public static User Create(int? id, string login, string firstName, string lastName, string displayName,string competentProfile,
            string userPricipalName, string passwordHash,
            string phoneNumber, string email, Team assignedTeam,
            AccountStatusEnum status, ICollection<UserClaims> claims, DateTime? modificateDate)
        {
            return new User
            {
                Id = id,
                Login = login,
                FirstName = firstName,
                LastName = lastName,
                DisplayName = displayName,
                CompetentProfile = competentProfile,
                UserPricipalName = userPricipalName,
                PasswordHash = passwordHash,
                PhoneNumber = phoneNumber,
                Email = email,
                AssignedTeam = assignedTeam,
                Status = status,
                Claims = claims,
                ModificateDate = modificateDate,
            };
        }

        private User() { }

        public int? Id { get; set; }
        public string Login { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string DisplayName { get; private set; }
        public string CompetentProfile { get; private set; }
        public string UserPricipalName { get; private set; }
        public string PasswordHash { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public Team AssignedTeam { get; private set; }
        public AccountStatusEnum Status { get; private set; }
        public ICollection<UserClaims> Claims { get; private set; }
        public DateTime CreateDate { get; } = DateTime.Today;
        public DateTime? ModificateDate { get; private set; }

        public void UserHasLoged()
        {
            var @event = new UserHasLogedEvent(this);
            DomainEvents.Add(@event);
        }

    }
}
