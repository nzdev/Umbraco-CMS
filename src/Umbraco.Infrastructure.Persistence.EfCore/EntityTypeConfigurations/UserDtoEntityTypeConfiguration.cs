namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class UserDtoEntityTypeConfiguration : IEntityTypeConfiguration<UserDto>
    {
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
            builder.ToTable(UserDto.TableName);
            builder.HasKey(x => x.Id).HasName("PK_user");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Disabled).HasColumnName("userDisabled");
            builder.Property(x => x.Disabled).HasDefaultValue(0);
            builder.Property(x => x.NoConsole).HasColumnName("userNoConsole");
            builder.Property(x => x.NoConsole).HasDefaultValue(0);
            builder.Property(x => x.UserName).HasColumnName("userName");
            builder.Property(x => x.Login).HasColumnName("userLogin");
            builder.Property(x => x.Login).HasMaxLength(125);
            builder.HasIndex(x => x.Login);
            builder.Property(x => x.Password).HasColumnName("userPassword");
            builder.Property(x => x.Password).HasMaxLength(500);
            builder.Property(x => x.PasswordConfig).HasColumnName("passwordConfig");
            builder.Property(x => x.PasswordConfig).IsRequired(false);
            builder.Property(x => x.PasswordConfig).HasMaxLength(500);
            builder.Property(x => x.Email).HasColumnName("userEmail");
            builder.Property(x => x.UserLanguage).HasColumnName("userLanguage");
            builder.Property(x => x.UserLanguage).IsRequired(false);
            builder.Property(x => x.UserLanguage).HasMaxLength(10);
            builder.Property(x => x.SecurityStampToken).HasColumnName("securityStampToken");
            builder.Property(x => x.SecurityStampToken).IsRequired(false);
            builder.Property(x => x.SecurityStampToken).HasMaxLength(255);
            builder.Property(x => x.FailedLoginAttempts).HasColumnName("failedLoginAttempts");
            builder.Property(x => x.FailedLoginAttempts).IsRequired(false);
            builder.Property(x => x.LastLockoutDate).HasColumnName("lastLockoutDate");
            builder.Property(x => x.LastLockoutDate).IsRequired(false);
            builder.Property(x => x.LastPasswordChangeDate).HasColumnName("lastPasswordChangeDate");
            builder.Property(x => x.LastPasswordChangeDate).IsRequired(false);
            builder.Property(x => x.LastLoginDate).HasColumnName("lastLoginDate");
            builder.Property(x => x.LastLoginDate).IsRequired(false);
            builder.Property(x => x.EmailConfirmedDate).HasColumnName("emailConfirmedDate");
            builder.Property(x => x.EmailConfirmedDate).IsRequired(false);
            builder.Property(x => x.InvitedDate).HasColumnName("invitedDate");
            builder.Property(x => x.InvitedDate).IsRequired(false);
            builder.Property(x => x.CreateDate).HasColumnName("createDate");
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.UpdateDate).HasColumnName("updateDate");
            builder.Property(x => x.UpdateDate).IsRequired(true);
            builder.Property(x => x.UpdateDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.Avatar).HasColumnName("avatar");
            builder.Property(x => x.Avatar).IsRequired(false);
            builder.Property(x => x.Avatar).HasMaxLength(500);
            builder.Property(x => x.TourData).HasColumnName("tourData");
            builder.Property(x => x.TourData).IsRequired(false);
            builder.Property(x => x.TourData).HasColumnType("NTEXT");
            builder.HasMany(typeof(UserGroupDto), "UserId");
            builder.HasMany(typeof(UserStartNodeDto), "UserId");
        }
    }
}