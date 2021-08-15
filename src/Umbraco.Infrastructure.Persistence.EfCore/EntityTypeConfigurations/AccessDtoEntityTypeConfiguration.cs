namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class AccessDtoEntityTypeConfiguration : IEntityTypeConfiguration<AccessDto>
    {
        public void Configure(EntityTypeBuilder<AccessDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Access);
            builder.HasKey(x => x.Id).HasName("PK_umbracoAccess");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(NodeDto), "FK_umbracoAccess_umbracoNode_id").WithOne();
            builder.HasIndex(x => x.NodeId).IsUnique(true);
            builder.Property(x => x.LoginNodeId).HasColumnName("loginNodeId");
            builder.HasOne(typeof(NodeDto), "FK_umbracoAccess_umbracoNode_id1").WithOne();
            builder.Property(x => x.NoAccessNodeId).HasColumnName("noAccessNodeId");
            builder.HasOne(typeof(NodeDto), "FK_umbracoAccess_umbracoNode_id2").WithOne();
            builder.Property(x => x.CreateDate).HasColumnName("createDate");
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.UpdateDate).HasColumnName("updateDate");
            builder.Property(x => x.UpdateDate).HasDefaultValueSql("getdate()");
            builder.HasMany(typeof(AccessRuleDto), "AccessId");
        }
    }
}