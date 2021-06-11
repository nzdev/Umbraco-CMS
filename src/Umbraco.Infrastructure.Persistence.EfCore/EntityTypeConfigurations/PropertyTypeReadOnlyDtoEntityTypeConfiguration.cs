namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class PropertyTypeReadOnlyDtoEntityTypeConfiguration : IEntityTypeConfiguration<PropertyTypeReadOnlyDto>
    {
        public void Configure(EntityTypeBuilder<PropertyTypeReadOnlyDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.PropertyType);
            builder.Property(x => x.Id).HasColumnName("PropertyTypeId");
            builder.Property(x => x.DataTypeId).HasColumnName("dataTypeId");
            builder.Property(x => x.ContentTypeId).HasColumnName("contentTypeId");
            builder.Property(x => x.PropertyTypeGroupId).HasColumnName("PropertyTypesGroupId");
            builder.Property(x => x.Alias).HasColumnName("Alias");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.SortOrder).HasColumnName("PropertyTypeSortOrder");
            builder.Property(x => x.Mandatory).HasColumnName("mandatory");
            builder.Property(x => x.MandatoryMessage).HasColumnName("mandatoryMessage");
            builder.Property(x => x.ValidationRegExp).HasColumnName("validationRegExp");
            builder.Property(x => x.ValidationRegExpMessage).HasColumnName("validationRegExpMessage");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.LabelOnTop).HasColumnName("labelOnTop");
            builder.Property(x => x.CanEdit).HasColumnName("memberCanEdit");
            builder.Property(x => x.ViewOnProfile).HasColumnName("viewOnProfile");
            builder.Property(x => x.IsSensitive).HasColumnName("isSensitive");
            builder.Property(x => x.PropertyEditorAlias).HasColumnName("propertyEditorAlias");
            builder.Property(x => x.DbType).HasColumnName("dbType");
            builder.Property(x => x.UniqueId).HasColumnName("UniqueID");
        }
    }
}