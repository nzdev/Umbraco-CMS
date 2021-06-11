namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class MediaVersionDtoEntityTypeConfiguration : IEntityTypeConfiguration<MediaVersionDto>
    {
        public void Configure(EntityTypeBuilder<MediaVersionDto> builder)
        {
            builder.ToTable(MediaVersionDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Id).HasColumnName("id");
            builder.HasOne(typeof(ContentVersionDto)).WithOne();
            builder.HasIndex(x => x.Id).IsUnique(true);
            builder.Property(x => x.Path).HasColumnName("path");
            builder.Property(x => x.Path).IsRequired(false);
            builder.HasOne(typeof(ContentVersionDto), nameof(MediaVersionDto.ContentVersionDto));
        }
    }
}