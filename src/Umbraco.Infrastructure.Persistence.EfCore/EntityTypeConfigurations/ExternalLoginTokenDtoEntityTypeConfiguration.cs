namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ExternalLoginTokenDtoEntityTypeConfiguration : IEntityTypeConfiguration<ExternalLoginTokenDto>
    {
        public void Configure(EntityTypeBuilder<ExternalLoginTokenDto> builder)
        {
            builder.ToTable(ExternalLoginTokenDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ExternalLoginId).HasColumnName("externalLoginId");
            builder.HasOne(typeof(ExternalLoginDto)).WithOne();
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Name).HasMaxLength(255);
            builder.HasIndex(x => x.Name).IsUnique(true);
            builder.Property(x => x.Value).HasColumnName("value");
            builder.Property(x => x.Value).IsRequired(true);
            builder.Property(x => x.Value).HasColumnType("nvarchar(max)");
            builder.Property(x => x.CreateDate).HasColumnName("createDate");
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            builder.HasOne(typeof(ExternalLoginDto), nameof(ExternalLoginTokenDto.ExternalLoginDto));
        }
    }
}