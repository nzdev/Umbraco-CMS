namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class NodeDtoEntityTypeConfiguration : IEntityTypeConfiguration<NodeDto>
    {
        public void Configure(EntityTypeBuilder<NodeDto> builder)
        {
            builder.ToTable(NodeDto.TableName);
            builder.HasKey(x => x.NodeId);
            builder.Property(x => x.NodeId).HasDefaultValueSql("NEXT VALUE FOR dbo.NodeDto_seq");
            builder.Property(x => x.NodeId).HasColumnName("id");
            builder.Property(x => x.UniqueId).HasColumnName("uniqueId");
            builder.Property(x => x.UniqueId).IsRequired(true);
            builder.Property(x => x.UniqueId).HasDefaultValueSql("NEWID()");
            builder.HasIndex(x => x.UniqueId).IsUnique(true);
            builder.Property(x => x.ParentId).HasColumnName("parentId");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.HasIndex(x => x.ParentId);
            builder.Property(x => x.Level).HasColumnName("level");
            builder.Property(x => x.Path).HasColumnName("path");
            builder.Property(x => x.Path).HasMaxLength(150);
            builder.HasIndex(x => x.Path);
            builder.Property(x => x.SortOrder).HasColumnName("sortOrder");
            builder.Property(x => x.Trashed).HasColumnName("trashed");
            builder.Property(x => x.Trashed).HasDefaultValue(0);
            builder.HasIndex(x => x.Trashed);
            builder.Property(x => x.UserId).HasColumnName("nodeUser");
            builder.HasOne(typeof(UserDto)).WithOne();
            builder.Property(x => x.UserId).IsRequired(false);
            builder.Property(x => x.Text).HasColumnName("text");
            builder.Property(x => x.Text).IsRequired(false);
            builder.Property(x => x.NodeObjectType).HasColumnName("nodeObjectType");
            builder.Property(x => x.NodeObjectType).IsRequired(false);
            builder.HasIndex(x => x.NodeObjectType);
            builder.Property(x => x.CreateDate).HasColumnName("createDate");
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
        }
    }
}
