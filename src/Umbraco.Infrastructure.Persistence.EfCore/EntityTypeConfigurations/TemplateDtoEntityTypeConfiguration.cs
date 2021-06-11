namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class TemplateDtoEntityTypeConfiguration : IEntityTypeConfiguration<TemplateDto>
    {
        public void Configure(EntityTypeBuilder<TemplateDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Template);
            builder.HasKey(x => x.PrimaryKey);
            builder.Property(x => x.PrimaryKey).HasColumnName("pk");
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(NodeDto), "FK_cmsTemplate_umbracoNode").WithOne();
            builder.HasIndex(x => x.NodeId).IsUnique(true);
            builder.Property(x => x.Alias).HasColumnName("alias");
            builder.Property(x => x.Alias).IsRequired(false);
            builder.Property(x => x.Alias).HasMaxLength(100);
            builder.HasOne(typeof(NodeDto), nameof(TemplateDto.NodeDto));
        }
    }
}