namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class ServerRegistrationDtoEntityTypeConfiguration : IEntityTypeConfiguration<ServerRegistrationDto>
    {
        public void Configure(EntityTypeBuilder<ServerRegistrationDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Server);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ServerAddress).HasColumnName("address");
            builder.Property(x => x.ServerAddress).HasMaxLength(500);
            builder.Property(x => x.ServerIdentity).HasColumnName("computerName");
            builder.Property(x => x.ServerIdentity).HasMaxLength(255);
            builder.HasIndex(x => x.ServerIdentity).IsUnique(true);
            builder.Property(x => x.DateRegistered).HasColumnName("registeredDate");
            builder.Property(x => x.DateRegistered).HasDefaultValueSql("getdate()");
            builder.Property(x => x.DateAccessed).HasColumnName("lastNotifiedDate");
            builder.Property(x => x.IsActive).HasColumnName("isActive");
            builder.HasIndex(x => x.IsActive);
            builder.Property(x => x.IsMaster).HasColumnName("isMaster");
        }
    }
}