namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class PropertyTypeDtoEntityTypeConfiguration : IEntityTypeConfiguration<PropertyTypeDto>
    {
        public void Configure(EntityTypeBuilder<PropertyTypeDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.PropertyType);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEXT VALUE FOR dbo.PropertyTypeDto_seq");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.DataTypeId).HasColumnName("dataTypeId");
            builder.HasOne(typeof(DataTypeDto)).WithOne();
            builder.Property(x => x.ContentTypeId).HasColumnName("contentTypeId");
            builder.HasOne(typeof(ContentTypeDto)).WithOne();
            builder.Property(x => x.PropertyTypeGroupId).HasColumnName("propertyTypeGroupId");
            builder.HasOne(typeof(PropertyTypeGroupDto)).WithOne();
            builder.Property(x => x.PropertyTypeGroupId).IsRequired(false);
            builder.Property(x => x.Alias).HasColumnName("Alias");
            builder.HasIndex(x => x.Alias);
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.Name).IsRequired(false);
            builder.Property(x => x.SortOrder).HasColumnName("sortOrder");
            builder.Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Property(x => x.Mandatory).HasColumnName("mandatory");
            builder.Property(x => x.Mandatory).HasDefaultValue(0);
            builder.Property(x => x.MandatoryMessage).HasColumnName("mandatoryMessage");
            builder.Property(x => x.MandatoryMessage).IsRequired(false);
            builder.Property(x => x.MandatoryMessage).HasMaxLength(500);
            builder.Property(x => x.ValidationRegExp).HasColumnName("validationRegExp");
            builder.Property(x => x.ValidationRegExp).IsRequired(false);
            builder.Property(x => x.ValidationRegExpMessage).HasColumnName("validationRegExpMessage");
            builder.Property(x => x.ValidationRegExpMessage).IsRequired(false);
            builder.Property(x => x.ValidationRegExpMessage).HasMaxLength(500);
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.Property(x => x.LabelOnTop).HasColumnName("labelOnTop");
            builder.Property(x => x.LabelOnTop).HasDefaultValue(0);
            builder.Property(x => x.Variations).HasColumnName("variations");
            builder.Property(x => x.Variations).HasDefaultValue(1);
            builder.HasOne(typeof(DataTypeDto), nameof(PropertyTypeDto.DataTypeDto));
            builder.Property(x => x.UniqueId).HasColumnName("UniqueID");
            builder.Property(x => x.UniqueId).IsRequired(true);
            builder.Property(x => x.UniqueId).HasDefaultValueSql("NEWID()");
            builder.HasIndex(x => x.UniqueId).IsUnique(true);
        }
    }
}
