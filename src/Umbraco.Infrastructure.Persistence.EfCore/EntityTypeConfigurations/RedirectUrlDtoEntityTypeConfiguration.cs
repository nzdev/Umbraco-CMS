namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class RedirectUrlDtoEntityTypeConfiguration : IEntityTypeConfiguration<RedirectUrlDto>
    {
        public void Configure(EntityTypeBuilder<RedirectUrlDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.RedirectUrl);
            builder.HasKey(x => x.Id).HasName("PK_umbracoRedirectUrl");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ContentKey).HasColumnName("contentKey");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.Property(x => x.ContentKey).IsRequired(true);
            builder.Property(x => x.CreateDateUtc).HasColumnName("createDateUtc");
            builder.Property(x => x.CreateDateUtc).IsRequired(true);
            builder.Property(x => x.Url).HasColumnName("url");
            builder.Property(x => x.Url).IsRequired(true);
            builder.Property(x => x.Culture).HasColumnName("culture");
            builder.Property(x => x.Culture).IsRequired(false);
            builder.Property(x => x.UrlHash).HasColumnName("urlHash");
            builder.Property(x => x.UrlHash).IsRequired(true);
            builder.Property(x => x.UrlHash).HasMaxLength(40);
            builder.HasIndex(x => x.UrlHash).IsUnique(true);
        }
    }
}