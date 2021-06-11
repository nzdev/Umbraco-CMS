namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentVersionCultureVariationDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentVersionCultureVariationDto>
    {
        public void Configure(EntityTypeBuilder<ContentVersionCultureVariationDto> builder)
        {
            builder.ToTable(ContentVersionCultureVariationDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.VersionId).HasColumnName("versionId");
            builder.HasOne(typeof(ContentVersionDto)).WithOne();
            builder.HasIndex(x => x.VersionId).IsUnique(true);
            builder.Property(x => x.LanguageId).HasColumnName("languageId");
            builder.HasOne(typeof(LanguageDto)).WithOne();
            builder.HasIndex(x => x.LanguageId);
            builder.Ignore(x => x.Culture);
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.UpdateDate).HasColumnName("date");
            builder.Property(x => x.UpdateUserId).HasColumnName("availableUserId");
            builder.HasOne(typeof(UserDto)).WithOne();
            builder.Property(x => x.UpdateUserId).IsRequired(false);
        }
    }
}