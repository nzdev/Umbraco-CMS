namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class MemberDtoEntityTypeConfiguration : IEntityTypeConfiguration<MemberDto>
    {
        public void Configure(EntityTypeBuilder<MemberDto> builder)
        {
            builder.ToTable(MemberDto.TableName);
            builder.HasKey(x => x.NodeId);
            builder.Property(x => x.NodeId).ValueGeneratedNever();
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(ContentDto)).WithOne();
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Email).HasMaxLength(1000);
            builder.Property(x => x.Email).HasDefaultValueSql("''");
            builder.Property(x => x.LoginName).HasColumnName("LoginName");
            builder.Property(x => x.LoginName).HasMaxLength(1000);
            builder.Property(x => x.LoginName).HasDefaultValueSql("''");
            builder.HasIndex(x => x.LoginName);
            builder.Property(x => x.Password).HasColumnName("Password");
            builder.Property(x => x.Password).HasMaxLength(1000);
            builder.Property(x => x.Password).HasDefaultValueSql("''");
            builder.Property(x => x.PasswordConfig).HasColumnName("passwordConfig");
            builder.Property(x => x.PasswordConfig).IsRequired(false);
            builder.Property(x => x.PasswordConfig).HasMaxLength(500);
            builder.Property(x => x.SecurityStampToken).HasColumnName("securityStampToken");
            builder.Property(x => x.SecurityStampToken).IsRequired(false);
            builder.Property(x => x.SecurityStampToken).HasMaxLength(255);
            builder.Property(x => x.EmailConfirmedDate).HasColumnName("emailConfirmedDate");
            builder.Property(x => x.EmailConfirmedDate).IsRequired(false);
            builder.HasOne(typeof(ContentDto), nameof(MemberDto.ContentDto));
            builder.HasOne(typeof(ContentVersionDto), nameof(MemberDto.ContentVersionDto));
        }
    }
}