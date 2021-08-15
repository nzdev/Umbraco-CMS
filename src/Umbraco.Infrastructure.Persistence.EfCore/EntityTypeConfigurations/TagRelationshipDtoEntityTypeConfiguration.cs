namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class TagRelationshipDtoEntityTypeConfiguration : IEntityTypeConfiguration<TagRelationshipDto>
    {
        public void Configure(EntityTypeBuilder<TagRelationshipDto> builder)
        {
            builder.ToTable(TagRelationshipDto.TableName);
            builder.HasKey(x => new
            {
            x.NodeId, x.PropertyTypeId, x.TagId
            }).HasName("PK_cmsTagRelationship");
            builder.Property(x => x.NodeId).ValueGeneratedNever();
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(ContentDto), "FK_cmsTagRelationship_cmsContent").WithOne();
            builder.Property(x => x.TagId).HasColumnName("tagId");
            builder.HasOne(typeof(TagDto)).WithOne();
            builder.Property(x => x.PropertyTypeId).HasColumnName("propertyTypeId");
            builder.HasOne(typeof(PropertyTypeDto), "FK_cmsTagRelationship_cmsPropertyType").WithOne();
        }
    }
}