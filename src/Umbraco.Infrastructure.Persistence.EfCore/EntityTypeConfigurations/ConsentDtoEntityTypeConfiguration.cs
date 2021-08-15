namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ConsentDtoEntityTypeConfiguration : IEntityTypeConfiguration<ConsentDto>
    {
        public void Configure(EntityTypeBuilder<ConsentDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Consent);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Current).HasColumnName("current");
            builder.Property(x => x.Source).HasColumnName("source");
            builder.Property(x => x.Source).HasMaxLength(512);
            builder.Property(x => x.Context).HasColumnName("context");
            builder.Property(x => x.Context).HasMaxLength(128);
            builder.Property(x => x.Action).HasColumnName("action");
            builder.Property(x => x.Action).HasMaxLength(512);
            builder.Property(x => x.CreateDate).HasColumnName("createDate");
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.State).HasColumnName("state");
            builder.Property(x => x.Comment).HasColumnName("comment");
            builder.Property(x => x.Comment).IsRequired(false);
        }
    }
}