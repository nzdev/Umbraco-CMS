namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class LockDtoEntityTypeConfiguration : IEntityTypeConfiguration<LockDto>
    {
        public void Configure(EntityTypeBuilder<LockDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Lock);
            builder.HasKey(x => x.Id).HasName("PK_umbracoLock");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Value).HasColumnName("value");
            builder.Property(x => x.Value).IsRequired(true);
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Name).HasMaxLength(64);
        }
    }
}