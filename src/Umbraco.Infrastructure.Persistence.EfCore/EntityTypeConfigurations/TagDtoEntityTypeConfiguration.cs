namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class TagDtoEntityTypeConfiguration : IEntityTypeConfiguration<TagDto>
    {
        public void Configure(EntityTypeBuilder<TagDto> builder)
        {
            builder.ToTable(TagDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Group).HasColumnName("group");
            builder.Property(x => x.Group).HasMaxLength(100);
            builder.Property(x => x.LanguageId).HasColumnName("languageId");
            builder.HasOne(typeof(LanguageDto)).WithOne();
            builder.Property(x => x.LanguageId).IsRequired(false);
            builder.HasIndex(x => x.LanguageId);
            builder.Property(x => x.Text).HasColumnName("tag");
            builder.Property(x => x.Text).HasMaxLength(200);
            builder.HasIndex(x => x.Text).IsUnique(true);
        }
    }
}