namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class PropertyTypeGroupDtoEntityTypeConfiguration : IEntityTypeConfiguration<PropertyTypeGroupDto>
    {
        public void Configure(EntityTypeBuilder<PropertyTypeGroupDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.PropertyTypeGroup);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEXT VALUE FOR dbo.PropertyTypeGroupDto_seq");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ContentTypeNodeId).HasColumnName("contenttypeNodeId");
            builder.HasOne(typeof(ContentTypeDto)).WithOne();
            builder.Property(x => x.Text).HasColumnName("text");
            builder.Property(x => x.SortOrder).HasColumnName("sortorder");
            builder.HasMany(typeof(PropertyTypeDto), "PropertyTypeGroupId");
            builder.Property(x => x.UniqueId).HasColumnName("uniqueID");
            builder.Property(x => x.UniqueId).IsRequired(true);
            builder.Property(x => x.UniqueId).HasDefaultValueSql("NEWID()");
            builder.HasIndex(x => x.UniqueId).IsUnique(true);
        }
    }
}
