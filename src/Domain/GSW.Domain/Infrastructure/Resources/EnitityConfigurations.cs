using System;
using System.Collections.Generic;
using System.Text;
using GSW.Domain.Domain.Backlogs;
using GSW.Domain.Domain.Backlogs.Shared;
using GSW.Domain.Domain.Dictionaries;
using GSW.Domain.Domain.Teams;
using GSW.Domain.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GSW.Domain.Infrastructure.Resources
{
    public static class EnitityConfigurations
    {
        public static void Configure(this EntityTypeBuilder<User> builder)
        {
            builder.HasData(User.Create(1, "admin", "admin", "admin", "Gsw Admin","local admin",null,"********", null, null, null,
                AccountStatusEnum.Active, null, null));

            builder
                .Property(e => e.Status)
                .HasConversion(v => v.ToString(), v => Enum.Parse<AccountStatusEnum>(v, true));
        }

        public static void Configure(this EntityTypeBuilder<UserClaims> builder)
        {
            builder
                .Property(e => e.Role)
                .HasConversion(v => v.ToString(), v => Enum.Parse<RolesEnumType>(v, true));
        }

        public static void Configure(this EntityTypeBuilder<Backlog> builder)
        {
            builder
                  .Property(e => e.Priority)
                .HasConversion(v => v.ToString(), v => Enum.Parse<BacklogPriorityEnumType>(v, true));

            builder
                 .Property(e => e.Type)
                .HasConversion(v => v.ToString(), v => Enum.Parse<BacklogTypeEnum>(v, true));

            builder
                 .Property(e => e.Resolution)
                .HasConversion(v => v.ToString(), v => Enum.Parse<ResolutionEnumType>(v, true));

            builder
                .Property(e => e.Status)
                .HasConversion(v => v.ToString(), v => Enum.Parse<BacklogStatusEnum>(v, true));
            builder
                .Property(e => e.Priority)
                .HasConversion(v => v.ToString(), v => Enum.Parse<BacklogPriorityEnumType>(v, true));

            builder
                .Property(e => e.Type)
                .HasConversion(v => v.ToString(), v => Enum.Parse<BacklogTypeEnum>(v, true));

            builder
                 .Property(e => e.Resolution)
                .HasConversion(v => v.ToString(), v => Enum.Parse<ResolutionEnumType>(v, true));

            builder
                  .Property(e => e.Status)
                .HasConversion(v => v.ToString(), v => Enum.Parse<BacklogStatusEnum>(v, true));

        }

        public static void Configure(this EntityTypeBuilder<BacklogDictionary> builder)
        {
            builder
                .Property(e => e.Type)
                .HasConversion(v => v.ToString(), v => Enum.Parse<BacklogTypeEnum>(v, true));

            builder.HasData(new BacklogDictionary { Id = 1, Type = BacklogTypeEnum.Task });
        }

        public static void Configure(this EntityTypeBuilder<Team> builder)
        {
            builder.HasData(Team.Create(1, "Tools", 1, DateTime.Now, null));
        }
    }
}
