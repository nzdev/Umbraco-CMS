namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class RelationTypeDtoEntityTypeConfiguration : IEntityTypeConfiguration<RelationTypeDto>
    {
        public void Configure(EntityTypeBuilder<RelationTypeDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.RelationType);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEXT VALUE FOR dbo.RelationTypeDto_seq");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UniqueId).HasColumnName("typeUniqueId");
            builder.HasIndex(x => x.UniqueId).IsUnique(true);
            builder.Property(x => x.Dual).HasColumnName("dual");
            builder.Property(x => x.ParentObjectType).HasColumnName("parentObjectType");
            builder.Property(x => x.ParentObjectType).IsRequired(false);
            builder.Property(x => x.ChildObjectType).HasColumnName("childObjectType");
            builder.Property(x => x.ChildObjectType).IsRequired(false);
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Name).IsRequired(true);
            builder.HasIndex(x => x.Name).IsUnique(true);
            builder.Property(x => x.Alias).HasColumnName("alias");
            builder.Property(x => x.Alias).IsRequired(true);
            builder.Property(x => x.Alias).HasMaxLength(100);
            builder.HasIndex(x => x.Alias).IsUnique(true);
        }
    }
}
