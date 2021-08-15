namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentDto>
    {
        public void Configure(EntityTypeBuilder<ContentDto> builder)
        {
            builder.ToTable(ContentDto.TableName);
            builder.HasKey(x => x.NodeId);
            builder.Property(x => x.NodeId).ValueGeneratedNever();
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.Property(x => x.ContentTypeId).HasColumnName("contentTypeId");
            builder.HasOne(typeof(ContentTypeDto)).WithOne();
            builder.HasOne(typeof(NodeDto), nameof(ContentDto.NodeDto));
            builder.HasOne(typeof(ContentVersionDto), nameof(ContentDto.ContentVersionDto));
        }
    }
}