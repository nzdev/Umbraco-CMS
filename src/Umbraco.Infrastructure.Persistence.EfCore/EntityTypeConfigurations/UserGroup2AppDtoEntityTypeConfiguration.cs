namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class UserGroup2AppDtoEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup2AppDto>
    {
        public void Configure(EntityTypeBuilder<UserGroup2AppDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.UserGroup2App);
            builder.HasKey(x => new
            {
            x.UserGroupId, x.AppAlias
            }).HasName("PK_userGroup2App");
            builder.Property(x => x.UserGroupId).ValueGeneratedNever();
            builder.Property(x => x.UserGroupId).HasColumnName("userGroupId");
            builder.HasOne(typeof(UserGroupDto)).WithOne();
            builder.Property(x => x.AppAlias).HasColumnName("app");
            builder.Property(x => x.AppAlias).HasMaxLength(50);
        }
    }
}
