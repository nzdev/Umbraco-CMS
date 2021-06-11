namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class UserLoginDtoEntityTypeConfiguration : IEntityTypeConfiguration<UserLoginDto>
    {
        public void Configure(EntityTypeBuilder<UserLoginDto> builder)
        {
            builder.ToTable(UserLoginDto.TableName);
            builder.HasKey(x => x.SessionId);
            builder.Property(x => x.SessionId).ValueGeneratedNever();
            builder.Property(x => x.SessionId).HasColumnName("sessionId");
            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasOne(typeof(UserDto), "FK_" + UserLoginDto.TableName + "_umbracoUser_id").WithOne();
            builder.Property(x => x.LoggedInUtc).HasColumnName("loggedInUtc");
            builder.Property(x => x.LoggedInUtc).IsRequired(true);
            builder.Property(x => x.LastValidatedUtc).HasColumnName("lastValidatedUtc");
            builder.Property(x => x.LastValidatedUtc).IsRequired(true);
            builder.HasIndex(x => x.LastValidatedUtc);
            builder.Property(x => x.LoggedOutUtc).HasColumnName("loggedOutUtc");
            builder.Property(x => x.LoggedOutUtc).IsRequired(false);
            builder.Property(x => x.IpAddress).HasColumnName("ipAddress");
            builder.Property(x => x.IpAddress).IsRequired(false);
        }
    }
}
