namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class UserGroupDtoEntityTypeConfiguration : IEntityTypeConfiguration<UserGroupDto>
    {
        public void Configure(EntityTypeBuilder<UserGroupDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.UserGroup);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEXT VALUE FOR dbo.UserGroupDto_seq");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Alias).HasColumnName("userGroupAlias");
            builder.Property(x => x.Alias).HasMaxLength(200);
            builder.HasIndex(x => x.Alias).IsUnique(true);
            builder.Property(x => x.Name).HasColumnName("userGroupName");
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.HasIndex(x => x.Name).IsUnique(true);
            builder.Property(x => x.DefaultPermissions).HasColumnName("userGroupDefaultPermissions");
            builder.Property(x => x.DefaultPermissions).IsRequired(false);
            builder.Property(x => x.DefaultPermissions).HasMaxLength(50);
            builder.Property(x => x.CreateDate).HasColumnName("createDate");
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.UpdateDate).HasColumnName("updateDate");
            builder.Property(x => x.UpdateDate).IsRequired(true);
            builder.Property(x => x.UpdateDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.Icon).HasColumnName("icon");
            builder.Property(x => x.Icon).IsRequired(false);
            builder.Property(x => x.StartContentId).HasColumnName("startContentId");
            builder.HasOne(typeof(NodeDto), "FK_startContentId_umbracoNode_id").WithOne();
            builder.Property(x => x.StartContentId).IsRequired(false);
            builder.Property(x => x.StartMediaId).HasColumnName("startMediaId");
            builder.HasOne(typeof(NodeDto), "FK_startMediaId_umbracoNode_id").WithOne();
            builder.Property(x => x.StartMediaId).IsRequired(false);
            builder.HasMany(typeof(UserGroup2AppDto), "UserGroupId");
        }
    }
}
