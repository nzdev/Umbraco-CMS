namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class RelationDtoEntityTypeConfiguration : IEntityTypeConfiguration<RelationDto>
    {
        public void Configure(EntityTypeBuilder<RelationDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Relation);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ParentId).HasColumnName("parentId");
            builder.HasOne(typeof(NodeDto), "FK_umbracoRelation_umbracoNode").WithOne();
            builder.HasIndex(x => x.ParentId).IsUnique(true);
            builder.Property(x => x.ChildId).HasColumnName("childId");
            builder.HasOne(typeof(NodeDto), "FK_umbracoRelation_umbracoNode1").WithOne();
            builder.Property(x => x.RelationType).HasColumnName("relType");
            builder.HasOne(typeof(RelationTypeDto)).WithOne();
            builder.Property(x => x.Datetime).HasColumnName("datetime");
            builder.Property(x => x.Datetime).HasDefaultValueSql("getdate()");
            builder.Property(x => x.Comment).HasColumnName("comment");
            builder.Property(x => x.Comment).HasMaxLength(1000);
            builder.Property(x => x.ParentObjectType).HasColumnName("parentObjectType");
            builder.Property(x => x.ChildObjectType).HasColumnName("childObjectType");
        }
    }
}