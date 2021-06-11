namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class UserGroup2NodePermissionDtoEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup2NodePermissionDto>
    {
        public void Configure(EntityTypeBuilder<UserGroup2NodePermissionDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.UserGroup2NodePermission);
            builder.HasKey(x => new
            {
            x.UserGroupId, x.NodeId, x.Permission
            }).HasName("PK_umbracoUserGroup2NodePermission");
            builder.Property(x => x.UserGroupId).ValueGeneratedNever();
            builder.Property(x => x.UserGroupId).HasColumnName("userGroupId");
            builder.HasOne(typeof(UserGroupDto)).WithOne();
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.HasIndex(x => x.NodeId);
            builder.Property(x => x.Permission).HasColumnName("permission");
        }
    }
}