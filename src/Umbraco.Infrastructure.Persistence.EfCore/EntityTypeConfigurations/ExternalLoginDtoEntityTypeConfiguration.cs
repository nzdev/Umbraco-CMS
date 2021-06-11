namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ExternalLoginDtoEntityTypeConfiguration : IEntityTypeConfiguration<ExternalLoginDto>
    {
        public void Configure(EntityTypeBuilder<ExternalLoginDto> builder)
        {
            builder.ToTable(ExternalLoginDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasIndex(x => x.UserId);
            builder.Property(x => x.LoginProvider).HasColumnName("loginProvider");
            builder.Property(x => x.LoginProvider).IsRequired(true);
            builder.Property(x => x.LoginProvider).HasMaxLength(4000);
            builder.HasIndex(x => x.LoginProvider).IsUnique(true);
            builder.Property(x => x.ProviderKey).HasColumnName("providerKey");
            builder.Property(x => x.ProviderKey).IsRequired(true);
            builder.Property(x => x.ProviderKey).HasMaxLength(4000);
            builder.HasIndex(x => x.ProviderKey);
            builder.Property(x => x.CreateDate).HasColumnName("createDate");
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.UserData).HasColumnName("userData");
            builder.Property(x => x.UserData).IsRequired(false);
            builder.Property(x => x.UserData).HasColumnType("NTEXT");
        }
    }
}