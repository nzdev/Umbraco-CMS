namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class LanguageDtoEntityTypeConfiguration : IEntityTypeConfiguration<LanguageDto>
    {
        public void Configure(EntityTypeBuilder<LanguageDto> builder)
        {
            builder.ToTable(LanguageDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEXT VALUE FOR dbo.LanguageDto_seq");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.IsoCode).HasColumnName("languageISOCode");
            builder.Property(x => x.IsoCode).IsRequired(false);
            builder.Property(x => x.IsoCode).HasMaxLength(14);
            builder.HasIndex(x => x.IsoCode).IsUnique(true);
            builder.Property(x => x.CultureName).HasColumnName("languageCultureName");
            builder.Property(x => x.CultureName).IsRequired(false);
            builder.Property(x => x.CultureName).HasMaxLength(100);
            builder.Property(x => x.IsDefault).HasColumnName("isDefaultVariantLang");
            builder.Property(x => x.IsDefault).HasDefaultValue(0);
            builder.Property(x => x.IsMandatory).HasColumnName("mandatory");
            builder.Property(x => x.IsMandatory).HasDefaultValue(0);
            builder.Property(x => x.FallbackLanguageId).HasColumnName("fallbackLanguageId");
            builder.HasOne(typeof(LanguageDto)).WithOne();
            builder.Property(x => x.FallbackLanguageId).IsRequired(false);
            builder.HasIndex(x => x.FallbackLanguageId);
        }
    }
}
