namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentTypeTemplateDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentTypeTemplateDto>
    {
        public void Configure(EntityTypeBuilder<ContentTypeTemplateDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.DocumentType);
            builder.HasKey(x => new
            {
            x.ContentTypeNodeId, x.TemplateNodeId
            }).HasName("PK_cmsDocumentType");
            builder.Property(x => x.ContentTypeNodeId).ValueGeneratedNever();
            builder.Property(x => x.ContentTypeNodeId).HasColumnName("contentTypeNodeId");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.Property(x => x.TemplateNodeId).HasColumnName("templateNodeId");
            builder.HasOne(typeof(TemplateDto)).WithOne();
            builder.Property(x => x.IsDefault).HasColumnName("IsDefault");
            builder.Property(x => x.IsDefault).HasDefaultValue(0);
            builder.HasOne(typeof(ContentTypeDto), nameof(ContentTypeTemplateDto.ContentTypeDto));
        }
    }
}