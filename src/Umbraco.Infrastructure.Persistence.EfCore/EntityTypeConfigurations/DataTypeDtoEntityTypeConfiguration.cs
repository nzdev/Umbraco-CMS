namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class DataTypeDtoEntityTypeConfiguration : IEntityTypeConfiguration<DataTypeDto>
    {
        public void Configure(EntityTypeBuilder<DataTypeDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.DataType);
            builder.HasKey(x => x.NodeId);
            builder.Property(x => x.NodeId).ValueGeneratedNever();
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.Property(x => x.EditorAlias).HasColumnName("propertyEditorAlias");
            builder.Property(x => x.DbType).HasColumnName("dbType");
            builder.Property(x => x.DbType).HasMaxLength(50);
            builder.Property(x => x.Configuration).HasColumnName("config");
            builder.Property(x => x.Configuration).IsRequired(false);
            builder.Property(x => x.Configuration).HasColumnType("NTEXT");
            builder.HasOne(typeof(NodeDto), nameof(DataTypeDto.NodeDto));
        }
    }
}