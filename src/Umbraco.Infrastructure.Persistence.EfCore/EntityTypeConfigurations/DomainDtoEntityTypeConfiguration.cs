namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class DomainDtoEntityTypeConfiguration : IEntityTypeConfiguration<DomainDto>
    {
        public void Configure(EntityTypeBuilder<DomainDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Domain);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.DefaultLanguage).HasColumnName("domainDefaultLanguage");
            builder.Property(x => x.DefaultLanguage).IsRequired(false);
            builder.Property(x => x.RootStructureId).HasColumnName("domainRootStructureID");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.Property(x => x.RootStructureId).IsRequired(false);
            builder.Property(x => x.DomainName).HasColumnName("domainName");
        }
    }
}