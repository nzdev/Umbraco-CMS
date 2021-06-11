namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class CacheInstructionDtoEntityTypeConfiguration : IEntityTypeConfiguration<CacheInstructionDto>
    {
        public void Configure(EntityTypeBuilder<CacheInstructionDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.CacheInstruction);
            builder.HasKey(x => x.Id).HasName("PK_umbracoCacheInstruction");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Id).IsRequired(true);
            builder.Property(x => x.UtcStamp).HasColumnName("utcStamp");
            builder.Property(x => x.UtcStamp).IsRequired(true);
            builder.Property(x => x.Instructions).HasColumnName("jsonInstruction");
            builder.Property(x => x.Instructions).IsRequired(true);
            builder.Property(x => x.Instructions).HasColumnType("NTEXT");
            builder.Property(x => x.OriginIdentity).HasColumnName("originated");
            builder.Property(x => x.OriginIdentity).IsRequired(true);
            builder.Property(x => x.OriginIdentity).HasMaxLength(500);
            builder.Property(x => x.InstructionCount).HasColumnName("instructionCount");
            builder.Property(x => x.InstructionCount).IsRequired(true);
            builder.Property(x => x.InstructionCount).HasDefaultValue(1);
        }
    }
}