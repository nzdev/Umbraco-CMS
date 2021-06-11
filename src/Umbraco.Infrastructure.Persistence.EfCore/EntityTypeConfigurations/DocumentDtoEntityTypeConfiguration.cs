namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class DocumentDtoEntityTypeConfiguration : IEntityTypeConfiguration<DocumentDto>
    {
        public void Configure(EntityTypeBuilder<DocumentDto> builder)
        {
            builder.ToTable(DocumentDto.TableName);
            builder.HasKey(x => x.NodeId);
            builder.Property(x => x.NodeId).ValueGeneratedNever();
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(ContentDto)).WithOne();
            builder.Property(x => x.Published).HasColumnName("published");
            builder.HasIndex(x => x.Published);
            builder.Property(x => x.Edited).HasColumnName("edited");
            builder.HasOne(typeof(ContentDto), nameof(DocumentDto.ContentDto));
            builder.HasOne(typeof(DocumentVersionDto), nameof(DocumentDto.DocumentVersionDto));
            builder.HasOne(typeof(DocumentVersionDto), nameof(DocumentDto.PublishedVersionDto));
        }
    }
}
