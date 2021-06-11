namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class LogDtoEntityTypeConfiguration : IEntityTypeConfiguration<LogDto>
    {
        public void Configure(EntityTypeBuilder<LogDto> builder)
        {
            builder.ToTable(LogDto.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasOne(typeof(UserDto)).WithOne();
            builder.Property(x => x.UserId).IsRequired(false);
            builder.Property(x => x.NodeId).HasColumnName("NodeId");
            builder.HasIndex(x => x.NodeId);
            builder.Property(x => x.EntityType).HasColumnName("entityType");
            builder.Property(x => x.EntityType).IsRequired(false);
            builder.Property(x => x.EntityType).HasMaxLength(50);
            builder.Property(x => x.Datestamp).HasColumnName("Datestamp");
            builder.Property(x => x.Datestamp).HasDefaultValueSql("getdate()");
            builder.Property(x => x.Header).HasColumnName("logHeader");
            builder.Property(x => x.Header).HasMaxLength(50);
            builder.Property(x => x.Comment).HasColumnName("logComment");
            builder.Property(x => x.Comment).IsRequired(false);
            builder.Property(x => x.Comment).HasMaxLength(4000);
            builder.Property(x => x.Parameters).HasColumnName("parameters");
            builder.Property(x => x.Parameters).IsRequired(false);
            builder.Property(x => x.Parameters).HasMaxLength(500);
        }
    }
}