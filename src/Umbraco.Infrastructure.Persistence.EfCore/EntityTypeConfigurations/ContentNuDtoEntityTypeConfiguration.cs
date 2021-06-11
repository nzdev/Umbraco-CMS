namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentNuDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentNuDto>
    {
        public void Configure(EntityTypeBuilder<ContentNuDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.NodeData);
            builder.HasKey(x => new
            {
            x.NodeId, x.Published
            }).HasName("PK_cmsContentNu");
            builder.Property(x => x.NodeId).ValueGeneratedNever();
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(ContentDto)).WithOne();
            builder.Property(x => x.Published).HasColumnName("published");
            builder.Property(x => x.Data).HasColumnName("data");
            builder.Property(x => x.Data).HasColumnType("NTEXT");
            builder.Property(x => x.Rv).HasColumnName("rv");
        }
    }
}