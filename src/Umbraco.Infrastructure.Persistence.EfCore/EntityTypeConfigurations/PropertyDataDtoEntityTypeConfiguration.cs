namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class PropertyDataDtoEntityTypeConfiguration : IEntityTypeConfiguration<PropertyDataDto>
    {
        public void Configure(EntityTypeBuilder<PropertyDataDto> builder)
        {
            builder.ToTable(PropertyDataDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.VersionId).HasColumnName("versionId");
            builder.HasOne(typeof(ContentVersionDto)).WithOne();
            builder.HasIndex(x => x.VersionId).IsUnique(true);
            builder.Property(x => x.PropertyTypeId).HasColumnName("propertyTypeId");
            builder.HasOne(typeof(PropertyTypeDto)).WithOne();
            builder.HasIndex(x => x.PropertyTypeId);
            builder.Property(x => x.LanguageId).HasColumnName("languageId");
            builder.HasOne(typeof(LanguageDto)).WithOne();
            builder.Property(x => x.LanguageId).IsRequired(false);
            builder.HasIndex(x => x.LanguageId);
            builder.Property(x => x.Segment).HasColumnName("segment");
            builder.Property(x => x.Segment).IsRequired(false);
            builder.HasIndex(x => x.Segment);
            builder.Property(x => x.IntegerValue).HasColumnName("intValue");
            builder.Property(x => x.IntegerValue).IsRequired(false);
            builder.Property(x => x.DecimalValue).HasColumnName("decimalValue");
            builder.Property(x => x.DecimalValue).IsRequired(false);
            builder.Property(x => x.DateValue).HasColumnName("dateValue");
            builder.Property(x => x.DateValue).IsRequired(false);
            builder.Property(x => x.VarcharValue).HasColumnName("varcharValue");
            builder.Property(x => x.VarcharValue).IsRequired(false);
            builder.Property(x => x.TextValue).HasColumnName("textValue");
            builder.Property(x => x.TextValue).IsRequired(false);
            builder.Property(x => x.TextValue).HasColumnType("NTEXT");
            builder.HasOne(typeof(PropertyTypeDto), nameof(PropertyDataDto.PropertyTypeDto));
            builder.Ignore(x => x.Value);
        }
    }
}