namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ContentType2ContentTypeDtoEntityTypeConfiguration : IEntityTypeConfiguration<ContentType2ContentTypeDto>
    {
        public void Configure(EntityTypeBuilder<ContentType2ContentTypeDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.ElementTypeTree);
            builder.HasKey(x => new
            {
            x.ParentId, x.ChildId
            }).HasName("PK_cmsContentType2ContentType");
            builder.Property(x => x.ParentId).ValueGeneratedNever();
            builder.Property(x => x.ParentId).HasColumnName("parentContentTypeId");
            builder.HasOne(typeof(NodeDto), "FK_cmsContentType2ContentType_umbracoNode_parent").WithOne();
            builder.Property(x => x.ChildId).HasColumnName("childContentTypeId");
            builder.HasOne(typeof(NodeDto), "FK_cmsContentType2ContentType_umbracoNode_child").WithOne();
        }
    }
}
