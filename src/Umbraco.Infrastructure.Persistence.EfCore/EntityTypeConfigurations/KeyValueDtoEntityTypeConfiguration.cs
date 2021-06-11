namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class KeyValueDtoEntityTypeConfiguration : IEntityTypeConfiguration<KeyValueDto>
    {
        public void Configure(EntityTypeBuilder<KeyValueDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.KeyValue);
            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).ValueGeneratedNever();
            builder.Property(x => x.Key).HasColumnName("key");
            builder.Property(x => x.Key).HasMaxLength(256);
            builder.Property(x => x.Value).HasColumnName("value");
            builder.Property(x => x.Value).IsRequired(false);
            builder.Property(x => x.UpdateDate).HasColumnName("updated");
            builder.Property(x => x.UpdateDate).HasDefaultValueSql("getdate()");
        }
    }
}