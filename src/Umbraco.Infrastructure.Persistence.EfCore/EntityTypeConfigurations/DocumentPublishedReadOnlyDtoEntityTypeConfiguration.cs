namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class DocumentPublishedReadOnlyDtoEntityTypeConfiguration : IEntityTypeConfiguration<DocumentPublishedReadOnlyDto>
    {
        public void Configure(EntityTypeBuilder<DocumentPublishedReadOnlyDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Document);
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.Property(x => x.Published).HasColumnName("published");
            builder.Property(x => x.VersionId).HasColumnName("versionId");
            builder.Property(x => x.Newest).HasColumnName("newest");
            builder.Property(x => x.VersionDate).HasColumnName("updateDate");
        }
    }
}