namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class LogViewerQueryDtoEntityTypeConfiguration : IEntityTypeConfiguration<LogViewerQueryDto>
    {
        public void Configure(EntityTypeBuilder<LogViewerQueryDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.LogViewerQuery);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.HasIndex(x => x.Name).IsUnique(true);
            builder.Property(x => x.Query).HasColumnName("query");
        }
    }
}