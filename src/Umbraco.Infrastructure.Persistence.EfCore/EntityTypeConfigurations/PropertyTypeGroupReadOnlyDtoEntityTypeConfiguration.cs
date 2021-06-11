namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class PropertyTypeGroupReadOnlyDtoEntityTypeConfiguration : IEntityTypeConfiguration<PropertyTypeGroupReadOnlyDto>
    {
        public void Configure(EntityTypeBuilder<PropertyTypeGroupReadOnlyDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.PropertyTypeGroup);
            builder.Property(x => x.Id).HasColumnName("PropertyTypeGroupId");
            builder.Property(x => x.Text).HasColumnName("PropertyGroupName");
            builder.Property(x => x.SortOrder).HasColumnName("PropertyGroupSortOrder");
            builder.Property(x => x.ContentTypeNodeId).HasColumnName("contenttypeNodeId");
            builder.Property(x => x.UniqueId).HasColumnName("PropertyGroupUniqueID");
        }
    }
}