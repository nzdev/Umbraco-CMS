namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class LanguageTextDtoEntityTypeConfiguration : IEntityTypeConfiguration<LanguageTextDto>
    {
        public void Configure(EntityTypeBuilder<LanguageTextDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.DictionaryValue);
            builder.HasKey(x => x.PrimaryKey);
            builder.Property(x => x.PrimaryKey).HasColumnName("pk");
            builder.Property(x => x.LanguageId).HasColumnName("languageId");
            builder.HasOne(typeof(LanguageDto)).WithOne();
            builder.Property(x => x.UniqueId).HasColumnName("UniqueId");
            builder.HasOne(typeof(DictionaryDto)).WithOne();
            builder.Property(x => x.Value).HasColumnName("value");
            builder.Property(x => x.Value).HasMaxLength(1000);
        }
    }
}