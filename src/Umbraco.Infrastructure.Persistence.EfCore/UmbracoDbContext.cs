using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;

namespace Umbraco.Infrastructure.Persistence.EfCore
{
    internal class UmbracoDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasSequence<int>("UserGroupDto_seq", schema: "dbo").StartsAt(6).IncrementsBy(1);
            modelBuilder.HasSequence<int>("RelationTypeDto_seq", schema: "dbo").StartsAt(RelationTypeDto.NodeIdSeed).IncrementsBy(1);
            modelBuilder.HasSequence<int>("PropertyTypeGroupDto_seq", schema: "dbo").StartsAt(12).IncrementsBy(1);
            modelBuilder.HasSequence<int>("PropertyTypeDto_seq", schema: "dbo").StartsAt(100).IncrementsBy(1);
            modelBuilder.HasSequence<int>("NodeDto_seq", schema: "dbo").StartsAt(NodeDto.NodeIdSeed).IncrementsBy(1);
            modelBuilder.HasSequence<int>("LanguageDto_seq", schema: "dbo").StartsAt(2).IncrementsBy(1);
            modelBuilder.HasSequence<int>("ContentTypeDto_seq", schema: "dbo").StartsAt(700).IncrementsBy(1);
        }
    }
}
