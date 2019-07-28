using System.Collections.Generic;
using System.Linq;
using Database.EF;
using DDD.Interfaces;
using GSW.Domain.Domain.Backlogs;
using GSW.Domain.Domain.Dictionaries;
using GSW.Domain.Domain.Teams;
using GSW.Domain.Domain.Users;
using GSW.Domain.Infrastructure.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GSW.Domain.Infrastructure.DatabaseContext
{
    public class GswContext : DbContext
    {
        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly SqlSettings _sqlOptions;

        public virtual DbSet<User> Users { get; set; }
        public virtual  DbSet<UserClaims> UserClaims { get; set; }
        public virtual  DbSet<Backlog> Backlogs { get; set; }
        public GswContext(IOptions<SqlSettings> sqlOptions, IDomainEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
            _sqlOptions = sqlOptions.Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_sqlOptions.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_sqlOptions.Database);
                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.ConnectionString, b=> b.MigrationsAssembly(_sqlOptions.MigrationInfo));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Configure();
            modelBuilder.Entity<BacklogDictionary>().Configure();
            modelBuilder.Entity<UserClaims>().Configure();
            modelBuilder.Entity<Team>().Configure();
            modelBuilder.Entity<Backlog>().Configure();
        }

        public override int SaveChanges()
        {
            var numberOfChanges = base.SaveChanges();

            var entities = GetDomainEventEntities().Where(p => p.DomainEvents.Any()).ToList();

            entities?.ForEach(p =>
            {
                p.DomainEvents.ToList()?
                    .ForEach(
                        @event => _eventDispatcher.DispatchEvents(@event)
                    );
            });

            return numberOfChanges;
        }

        private IEnumerable<IAggregateRoot> GetDomainEventEntities()
        {
            return ChangeTracker.Entries<IAggregateRoot>()
                .Select(po => po.Entity)
                .Where(p => p.DomainEvents.Any())
                .ToArray();
        }
    }
}
