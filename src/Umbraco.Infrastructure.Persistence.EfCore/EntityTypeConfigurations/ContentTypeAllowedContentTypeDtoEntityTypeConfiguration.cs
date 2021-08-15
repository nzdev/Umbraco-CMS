namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentTypeAllowedContentTypeDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentTypeAllowedContentTypeDto>
    {
        public void Configure(EntityTypeBuilder<ContentTypeAllowedContentTypeDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.ContentChildType);
            builder.HasKey(x => new
            {
            x.Id, x.AllowedId
            }).HasName("PK_cmsContentTypeAllowedContentType");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.HasOne(typeof(ContentTypeDto), "FK_cmsContentTypeAllowedContentType_cmsContentType").WithOne();
            builder.Property(x => x.AllowedId).HasColumnName("AllowedId");
            builder.HasOne(typeof(ContentTypeDto), "FK_cmsContentTypeAllowedContentType_cmsContentType1").WithOne();
            builder.Property(x => x.SortOrder).HasColumnName("SortOrder");
            builder.Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Property(x => x.SortOrder).HasDefaultValue(0);
            builder.HasOne(typeof(ContentTypeDto), nameof(ContentTypeAllowedContentTypeDto.ContentTypeDto));
        }
    }
}