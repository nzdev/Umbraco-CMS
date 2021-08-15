namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class DocumentVersionDtoEntityTypeConfiguration : IEntityTypeConfiguration<DocumentVersionDto>
    {
        public void Configure(EntityTypeBuilder<DocumentVersionDto> builder)
        {
            builder.ToTable(DocumentVersionDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Id).HasColumnName("id");
            builder.HasOne(typeof(ContentVersionDto)).WithOne();
            builder.Property(x => x.TemplateId).HasColumnName("templateId");
            builder.HasOne(typeof(TemplateDto)).WithOne();
            builder.Property(x => x.TemplateId).IsRequired(false);
            builder.Property(x => x.Published).HasColumnName("published");
            builder.HasOne(typeof(ContentVersionDto), nameof(DocumentVersionDto.ContentVersionDto));
        }
    }
}