namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentVersionDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentVersionDto>
    {
        public void Configure(EntityTypeBuilder<ContentVersionDto> builder)
        {
            builder.ToTable(ContentVersionDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(ContentDto)).WithOne();
            builder.HasIndex(x => x.NodeId);
            builder.Property(x => x.VersionDate).HasColumnName("versionDate");
            builder.Property(x => x.VersionDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasOne(typeof(UserDto)).WithOne();
            builder.Property(x => x.UserId).IsRequired(false);
            builder.Property(x => x.Current).HasColumnName("current");
            builder.Property(x => x.Text).HasColumnName("text");
            builder.Property(x => x.Text).IsRequired(false);
            builder.HasOne(typeof(ContentDto), nameof(ContentVersionDto.ContentDto));
        }
    }
}