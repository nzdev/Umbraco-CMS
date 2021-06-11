namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentScheduleDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentScheduleDto>
    {
        public void Configure(EntityTypeBuilder<ContentScheduleDto> builder)
        {
            builder.ToTable(ContentScheduleDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(ContentDto)).WithOne();
            builder.Property(x => x.LanguageId).HasColumnName("languageId");
            builder.HasOne(typeof(LanguageDto)).WithOne();
            builder.Property(x => x.LanguageId).IsRequired(false);
            builder.Property(x => x.Date).HasColumnName("date");
            builder.Property(x => x.Action).HasColumnName("action");
        }
    }
}