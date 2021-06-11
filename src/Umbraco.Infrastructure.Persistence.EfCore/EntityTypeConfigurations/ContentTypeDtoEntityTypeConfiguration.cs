namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentTypeDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentTypeDto>
    {
        public void Configure(EntityTypeBuilder<ContentTypeDto> builder)
        {
            builder.ToTable(ContentTypeDto.TableName);
            builder.HasKey(x => x.PrimaryKey);
            builder.Property(x => x.PrimaryKey).HasDefaultValueSql("NEXT VALUE FOR dbo.ContentTypeDto_seq");
            builder.Property(x => x.PrimaryKey).HasColumnName("pk");
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.HasIndex(x => x.NodeId).IsUnique(true);
            builder.Property(x => x.Alias).HasColumnName("alias");
            builder.Property(x => x.Alias).IsRequired(false);
            builder.Property(x => x.Icon).HasColumnName("icon");
            builder.Property(x => x.Icon).IsRequired(false);
            builder.HasIndex(x => x.Icon);
            builder.Property(x => x.Thumbnail).HasColumnName("thumbnail");
            builder.Property(x => x.Thumbnail).HasDefaultValueSql("folder.png");
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(1500);
            builder.Property(x => x.IsContainer).HasColumnName("isContainer");
            builder.Property(x => x.IsContainer).HasDefaultValue(0);
            builder.Property(x => x.IsElement).HasColumnName("isElement");
            builder.Property(x => x.IsElement).HasDefaultValue(0);
            builder.Property(x => x.AllowAtRoot).HasColumnName("allowAtRoot");
            builder.Property(x => x.AllowAtRoot).HasDefaultValue(0);
            builder.Property(x => x.Variations).HasColumnName("variations");
            builder.Property(x => x.Variations).HasDefaultValue(1);
            builder.HasOne(typeof(NodeDto), nameof(ContentTypeDto.NodeDto));
        }
    }
}
