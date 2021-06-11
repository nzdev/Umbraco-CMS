namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class AuditEntryDtoEntityTypeConfiguration : IEntityTypeConfiguration<AuditEntryDto>
    {
        public void Configure(EntityTypeBuilder<AuditEntryDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.AuditEntry);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.PerformingUserId).HasColumnName("performingUserId");
            builder.Property(x => x.PerformingDetails).HasColumnName("performingDetails");
            builder.Property(x => x.PerformingDetails).IsRequired(false);
            builder.Property(x => x.PerformingIp).HasColumnName("performingIp");
            builder.Property(x => x.PerformingIp).IsRequired(false);
            builder.Property(x => x.EventDateUtc).HasColumnName("eventDateUtc");
            builder.Property(x => x.EventDateUtc).HasDefaultValueSql("getdate()");
            builder.Property(x => x.AffectedUserId).HasColumnName("affectedUserId");
            builder.Property(x => x.AffectedDetails).HasColumnName("affectedDetails");
            builder.Property(x => x.AffectedDetails).IsRequired(false);
            builder.Property(x => x.EventType).HasColumnName("eventType");
            builder.Property(x => x.EventDetails).HasColumnName("eventDetails");
            builder.Property(x => x.EventDetails).IsRequired(false);
        }
    }
}