namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class DictionaryDtoEntityTypeConfiguration : IEntityTypeConfiguration<DictionaryDto>
    {
        public void Configure(EntityTypeBuilder<DictionaryDto> builder)
        {
            builder.ToTable(DictionaryDto.TableName);
            builder.HasKey(x => x.PrimaryKey);
            builder.Property(x => x.PrimaryKey).HasColumnName("pk");
            builder.Property(x => x.UniqueId).HasColumnName("id");
            builder.HasIndex(x => x.UniqueId).IsUnique(true);
            builder.Property(x => x.Parent).HasColumnName("parent");
            builder.HasOne(typeof(DictionaryDto)).WithOne();
            builder.Property(x => x.Parent).IsRequired(false);
            builder.HasIndex(x => x.Parent);
            builder.Property(x => x.Key).HasColumnName("key");
            builder.Property(x => x.Key).HasMaxLength(450);
            builder.HasIndex(x => x.Key);
            builder.HasMany(typeof(LanguageTextDto), "UniqueId");
        }
    }
}