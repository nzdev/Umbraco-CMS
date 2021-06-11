namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class User2UserGroupDtoEntityTypeConfiguration : IEntityTypeConfiguration<User2UserGroupDto>
    {
        public void Configure(EntityTypeBuilder<User2UserGroupDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.User2UserGroup);
            builder.HasKey(x => new
            {
            x.UserId, x.UserGroupId
            }).HasName("PK_user2userGroup");
            builder.Property(x => x.UserId).ValueGeneratedNever();
            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasOne(typeof(UserDto)).WithOne();
            builder.Property(x => x.UserGroupId).HasColumnName("userGroupId");
            builder.HasOne(typeof(UserGroupDto)).WithOne();
        }
    }
}