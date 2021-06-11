namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class DocumentCultureVariationDtoEntityTypeConfiguration : IEntityTypeConfiguration<DocumentCultureVariationDto>
    {
        public void Configure(EntityTypeBuilder<DocumentCultureVariationDto> builder)
        {
            builder.ToTable(DocumentCultureVariationDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.HasIndex(x => x.NodeId).IsUnique(true);
            builder.Property(x => x.LanguageId).HasColumnName("languageId");
            builder.HasOne(typeof(LanguageDto)).WithOne();
            builder.HasIndex(x => x.LanguageId);
            builder.Ignore(x => x.Culture);
            builder.Property(x => x.Edited).HasColumnName("edited");
            builder.Property(x => x.Available).HasColumnName("available");
            builder.Property(x => x.Published).HasColumnName("published");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Name).IsRequired(false);
        }
    }
}